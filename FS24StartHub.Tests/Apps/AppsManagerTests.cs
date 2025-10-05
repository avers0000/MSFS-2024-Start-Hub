using FS24StartHub.Core.Domain;
using FS24StartHub.Core.Logging;
using FS24StartHub.Core.Settings;
using FS24StartHub.Infrastructure.Apps;
using Moq;

namespace FS24StartHub.Tests.Apps
{
    [TestClass]
    public class AppsManagerTests
    {
        private Mock<ISettingsManager> _settingsManagerMock = null!;
        private Mock<ILogManager> _logManagerMock = null!;
        private AppsManager _appsManager = null!;

        [TestInitialize]
        public void Setup()
        {
            _settingsManagerMock = new Mock<ISettingsManager>();
            _logManagerMock = new Mock<ILogManager>();

            _settingsManagerMock.Setup(s => s.CurrentSettings).Returns(new AppSettings
            {
                StartupItems = new List<StartupItem>()
            });

            _appsManager = new AppsManager(_settingsManagerMock.Object, _logManagerMock.Object);
        }

        [TestMethod]
        public void AddStartupItem_ShouldAddItemToCorrectList()
        {
            // Arrange
            var newItem = new StartupItem
            {
                Path = "C:\\example.exe",
                RunOption = RunOption.BeforeSimStarts,
                Enabled = true
            };

            // Act
            _appsManager.AddStartupItem(newItem);

            // Assert
            var items = _appsManager.GetStartupItems(RunOption.BeforeSimStarts).ToList();
            Assert.AreEqual(1, items.Count);
            Assert.AreEqual("C:\\example.exe", items[0].Path);
        }

        [TestMethod]
        public void UpdateStartupItem_ShouldUpdateExistingItem()
        {
            // Arrange
            var existingItem = new StartupItem
            {
                Id = Guid.NewGuid().ToString(),
                Path = "C:\\example.exe",
                RunOption = RunOption.BeforeSimStarts,
                Enabled = true
            };
            _appsManager.AddStartupItem(existingItem);

            var updatedItem = new StartupItem
            {
                Id = existingItem.Id,
                Path = "C:\\updated.exe",
                RunOption = RunOption.BeforeSimStarts,
                Enabled = false
            };

            // Act
            _appsManager.UpdateStartupItem(updatedItem);

            // Assert
            var items = _appsManager.GetStartupItems(RunOption.BeforeSimStarts).ToList();
            Assert.AreEqual(1, items.Count);
            Assert.AreEqual("C:\\updated.exe", items[0].Path);
            Assert.IsFalse(items[0].Enabled);
        }

        [TestMethod]
        public void RemoveStartupItem_ShouldRemoveItemFromList()
        {
            // Arrange
            var item = new StartupItem
            {
                Id = Guid.NewGuid().ToString(),
                Path = "C:\\example.exe",
                RunOption = RunOption.BeforeSimStarts,
                Enabled = true
            };
            _appsManager.AddStartupItem(item);

            // Act
            _appsManager.RemoveStartupItem(item.Id);

            // Assert
            var items = _appsManager.GetStartupItems(RunOption.BeforeSimStarts).ToList();
            Assert.AreEqual(0, items.Count);
        }

        [TestMethod]
        public void MoveStartupItem_ShouldChangeItemOrder()
        {
            // Arrange
            var item1 = new StartupItem { Id = Guid.NewGuid().ToString(), Path = "C:\\item1.exe", RunOption = RunOption.BeforeSimStarts, Order = 1 };
            var item2 = new StartupItem { Id = Guid.NewGuid().ToString(), Path = "C:\\item2.exe", RunOption = RunOption.BeforeSimStarts, Order = 2 };
            var item3 = new StartupItem { Id = Guid.NewGuid().ToString(), Path = "C:\\item3.exe", RunOption = RunOption.BeforeSimStarts, Order = 3 };
            _appsManager.AddStartupItem(item1);
            _appsManager.AddStartupItem(item2);
            _appsManager.AddStartupItem(item3);

            // Act: Move item3 up by one position
            _appsManager.MoveStartupItem(item3.Id, moveDown: false);

            // Assert
            var items = _appsManager.GetStartupItems(RunOption.BeforeSimStarts).ToList();
            Assert.AreEqual(3, items.Count);
            Assert.AreEqual("C:\\item1.exe", items[0].Path); // item1 should remain first
            Assert.AreEqual("C:\\item3.exe", items[1].Path); // item3 should now be second
            Assert.AreEqual("C:\\item2.exe", items[2].Path); // item2 should now be third
        }
    }
}