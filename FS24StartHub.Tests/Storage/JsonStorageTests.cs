using FS24StartHub.Core.Storage;
using FS24StartHub.Infrastructure.Storage;
using FS24StartHub.Core.Domain; // where AppSettings, CleanupSettings, etc. are defined
using Moq;
using System.Text.Json;

namespace FS24StartHub.Tests.Storage
{
    [TestClass]
    public class JsonStorageTests
    {
        private Mock<IFileStorage> _fileStorage = null!;
        private JsonStorage _jsonStorage = null!;

        [TestInitialize]
        public void Setup()
        {
            _fileStorage = new Mock<IFileStorage>();
            _jsonStorage = new JsonStorage(_fileStorage.Object);
        }

        [TestMethod]
        public void Save_ShouldSerializeObject_AndWriteToFile()
        {
            // Arrange
            var settings = new AppSettings
            {
                Language = "en",
                SimPath = "C:\\Sim",
                CurrentCareerId = "career1",
                CurrentConfigId = "config1",
                CleanupSettings = new CleanupSettings { CareerAutosaveLimit = 3, ConfigAutosaveLimit = 7 },
                Careers = new(),
                Configs = new(),
                StartupItems = new()
            };

            string? capturedPath = null;
            string? capturedContent = null;

            _fileStorage
                .Setup(fs => fs.WriteAllText(It.IsAny<string>(), It.IsAny<string>()))
                .Callback<string, string>((path, content) =>
                {
                    capturedPath = path;
                    capturedContent = content;
                });

            // Act
            _jsonStorage.Save("settings.json", settings);

            // Assert
            Assert.AreEqual("settings.json", capturedPath, "WriteAllText was called with an unexpected path.");
            Assert.IsNotNull(capturedContent, "WriteAllText was not called or content was null.");

            // Deserialize back and assert fields
            var roundTripped = JsonSerializer.Deserialize<AppSettings>(capturedContent!);
            Assert.IsNotNull(roundTripped, "Deserialized object is null.");

            Assert.AreEqual("en", roundTripped!.Language);
            Assert.AreEqual("C:\\Sim", roundTripped.SimPath);
            Assert.AreEqual("career1", roundTripped.CurrentCareerId);
            Assert.AreEqual("config1", roundTripped.CurrentConfigId);
            Assert.AreEqual(3, roundTripped.CleanupSettings.CareerAutosaveLimit);
            Assert.AreEqual(7, roundTripped.CleanupSettings.ConfigAutosaveLimit);
        }


        [TestMethod]
        public void Load_ShouldDeserializeObject_WhenFileContainsValidJson()
        {
            // Arrange
            var json = """
            {
              "Language": "ru",
              "SimPath": "D:\\Games\\Sim",
              "CurrentCareerId": "career42",
              "CurrentConfigId": "cfg99",
              "CleanupSettings": {
                "CareerAutosaveLimit": 5,
                "ConfigAutosaveLimit": 7
              },
              "Careers": [],
              "Configs": [],
              "StartupItems": []
            }
            """;

            _fileStorage
                .Setup(fs => fs.ReadAllText("settings.json"))
                .Returns(json);

            // Act
            var result = _jsonStorage.Load<AppSettings>("settings.json");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("ru", result.Language);
            Assert.AreEqual("D:\\Games\\Sim", result.SimPath);
            Assert.AreEqual("career42", result.CurrentCareerId);
            Assert.AreEqual(5, result.CleanupSettings.CareerAutosaveLimit);
            Assert.AreEqual(7, result.CleanupSettings.ConfigAutosaveLimit);
        }

        [TestMethod]
        [ExpectedException(typeof(JsonException))]
        public void Load_ShouldThrow_WhenJsonIsInvalid()
        {
            // Arrange
            _fileStorage
                .Setup(fs => fs.ReadAllText("settings.json"))
                .Returns("INVALID_JSON");

            // Act
            _jsonStorage.Load<AppSettings>("settings.json");
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void Load_ShouldThrow_WhenFileDoesNotExist()
        {
            // Arrange
            _fileStorage
                .Setup(fs => fs.ReadAllText("missing.json"))
                .Throws(new FileNotFoundException());

            // Act
            _jsonStorage.Load<AppSettings>("missing.json");
        }

        [TestMethod]
        public void TryLoad_ShouldReturnFalse_WhenFileDoesNotExist()
        {
            // Arrange
            _fileStorage
                .Setup(fs => fs.ReadAllText("missing.json"))
                .Throws(new FileNotFoundException());

            // Act
            var success = _jsonStorage.TryLoad<AppSettings>("missing.json", out var result);

            // Assert
            Assert.IsFalse(success);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TryLoad_ShouldReturnFalse_WhenJsonIsInvalid()
        {
            // Arrange
            _fileStorage
                .Setup(fs => fs.ReadAllText("settings.json"))
                .Returns("INVALID_JSON");

            // Act
            var success = _jsonStorage.TryLoad<AppSettings>("settings.json", out var result);

            // Assert
            Assert.IsFalse(success);
            Assert.IsNull(result);
        }
    }
}