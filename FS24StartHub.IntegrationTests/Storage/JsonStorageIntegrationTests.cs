using FS24StartHub.Core.Domain;
using FS24StartHub.Infrastructure.Storage;
using FS24StartHub.IntegrationTests.Helpers;

namespace FS24StartHub.IntegrationTests.Storage
{
    [TestClass]
    public class JsonStorageIntegrationTests
    {
        private string _baseFolder = null!;
        private string _filePath = null!;

        public TestContext TestContext { get; set; } = null!;

        [TestInitialize]
        public void Setup()
        {
            var externalRoot = TestPathHelper.GetExternalTestDataRoot();
            var testDataRoot = Path.Combine(externalRoot, "JsonStorage");

            _baseFolder = Path.Combine(testDataRoot, TestContext.TestName!);

            if (Directory.Exists(_baseFolder))
                Directory.Delete(_baseFolder, recursive: true);

            Directory.CreateDirectory(_baseFolder);

            _filePath = Path.Combine(_baseFolder, "settings.json");
        }

        [TestMethod]
        public void SaveAndLoad_RoundTrip_Works()
        {
            var fileStorage = new FileStorage();
            var jsonStorage = new JsonStorage(fileStorage);

            var settings = new AppSettings
            {
                Language = "en",
                SimPath = "C:\\Sim"
            };

            jsonStorage.Save(_filePath, settings);
            var success = jsonStorage.TryLoad<AppSettings>(_filePath, out var loaded);

            Assert.IsTrue(success, "File should be loaded successfully");
            Assert.IsNotNull(loaded);
            Assert.AreEqual("en", loaded!.Language);
            Assert.AreEqual("C:\\Sim", loaded.SimPath);
            Assert.IsTrue(File.Exists(_filePath), "Settings file should exist on disk");
        }

        [TestMethod]
        public void Save_OverwritesExistingFile()
        {
            var fileStorage = new FileStorage();
            var jsonStorage = new JsonStorage(fileStorage);

            var settings1 = new AppSettings { Language = "en", SimPath = "C:\\Sim1" };
            var settings2 = new AppSettings { Language = "de", SimPath = "D:\\Sim2" };

            jsonStorage.Save(_filePath, settings1);
            jsonStorage.Save(_filePath, settings2);

            var success = jsonStorage.TryLoad<AppSettings>(_filePath, out var loaded);

            Assert.IsTrue(success);
            Assert.AreEqual("de", loaded!.Language);
            Assert.AreEqual("D:\\Sim2", loaded.SimPath);
        }
    }
}