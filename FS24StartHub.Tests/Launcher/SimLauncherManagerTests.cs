using FS24StartHub.Core.Apps;
using FS24StartHub.Core.Configs;
using FS24StartHub.Core.Domain;
using FS24StartHub.Core.Launcher;
using FS24StartHub.Core.Launcher.Progress;
using FS24StartHub.Core.Launcher.Tasks;
using FS24StartHub.Core.Logging;
using FS24StartHub.Core.Settings;
using FS24StartHub.Infrastructure.Launcher;
using FS24StartHub.Infrastructure.Logging;
using Moq;

namespace FS24StartHub.Tests.Launcher
{
    [TestClass]
    public class SimLauncherManagerTests
    {
        private Mock<ILogManager> _logManagerMock = null!;
        private Mock<ISettingsManager> _settingsManagerMock = null!;
        private Mock<IAppsManager> _appsManagerMock = null!;
        private Mock<IConfigManager> _configManagerMock = null!;
        private Mock<ILaunchTask> _applyConfigTaskMock = null!;
        private Mock<ILaunchTask> _saveAppsTaskMock = null!;

        [TestInitialize]
        public void Setup()
        {
            _logManagerMock = new Mock<ILogManager>();
            _settingsManagerMock = new Mock<ISettingsManager>();
            _appsManagerMock = new Mock<IAppsManager>();
            _configManagerMock = new Mock<IConfigManager>();

            _applyConfigTaskMock = new Mock<ILaunchTask>();
            _applyConfigTaskMock.Setup(t => t.Name).Returns("ApplyConfigTask");
            _applyConfigTaskMock.Setup(t => t.IsOptional).Returns(false);
            _applyConfigTaskMock.Setup(t => t.ExecuteAsync(It.IsAny<LaunchRequest>(), It.IsAny<IProgress<StepProgress>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new StepProgress("ApplyConfigTask", ProgressType.StepCompleted, "Skipped", null, TimeSpan.Zero, true, null));
            _configManagerMock.Setup(m => m.GetSaveTask()).Returns(_applyConfigTaskMock.Object);

            _saveAppsTaskMock = new Mock<ILaunchTask>();
            _saveAppsTaskMock.Setup(t => t.Name).Returns("SaveTask");
            _saveAppsTaskMock.Setup(t => t.IsOptional).Returns(true);
            _saveAppsTaskMock.Setup(t => t.ExecuteAsync(It.IsAny<LaunchRequest>(), It.IsAny<IProgress<StepProgress>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new StepProgress("SaveTask", ProgressType.StepCompleted, "Completed", null, TimeSpan.Zero, true, null));
            _appsManagerMock.Setup(m => m.GetSaveTask()).Returns(_saveAppsTaskMock.Object);

            _appsManagerMock.Setup(m => m.GetTasks(It.IsAny<RunOption>())).Returns(Enumerable.Empty<ILaunchTask>());
        }

        private SimLauncherManager CreateManager() =>
            new SimLauncherManager(_logManagerMock.Object, _settingsManagerMock.Object, _appsManagerMock.Object, _configManagerMock.Object);

        [TestMethod]
        public async Task LaunchAsync_CancelledBeforeStart_ThrowsOperationCanceled()
        {
            // Arrange
            var logManager = new LogManager(new[] { new Mock<ILogSink>().Object });
            _settingsManagerMock.Setup(s => s.CurrentSettings).Returns(new AppSettings
            {
                SimType = SimType.Custom,
                SimExePath = "dummy.exe",
                LaunchTimeoutSeconds = 1
            });

            var manager = CreateManager();
            using var cts = new CancellationTokenSource();
            cts.Cancel();

            // Act + Assert
            await Assert.ThrowsExceptionAsync<OperationCanceledException>(
                () => manager.LaunchAsync(new LaunchRequest(), null, cts.Token));
        }

        [TestMethod]
        public async Task LaunchAsync_CancelledDuringExecution_ThrowsOperationCanceled()
        {
            // Arrange
            var longRunningTaskMock = new Mock<ILaunchTask>();
            longRunningTaskMock.Setup(t => t.Name).Returns("LongRunningTask");
            longRunningTaskMock.Setup(t => t.IsOptional).Returns(false);
            longRunningTaskMock.Setup(t => t.ExecuteAsync(It.IsAny<LaunchRequest>(), It.IsAny<IProgress<StepProgress>>(), It.IsAny<CancellationToken>()))
                .Returns(async (LaunchRequest request, IProgress<StepProgress> progress, CancellationToken ct) =>
                {
                    await Task.Delay(5000, ct);
                    return new StepProgress("LongRunningTask", ProgressType.StepCompleted, "Completed", null, TimeSpan.Zero, true, null);
                });

            _appsManagerMock.Setup(m => m.GetTasks(RunOption.BeforeSimStarts)).Returns(new[] { longRunningTaskMock.Object });

            var manager = CreateManager();
            var cts = new CancellationTokenSource();
            cts.CancelAfter(100);

            // Act + Assert
            try
            {
                await manager.LaunchAsync(new LaunchRequest(), null, cts.Token);
                Assert.Fail("Expected an OperationCanceledException to be thrown.");
            }
            catch (OperationCanceledException)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public async Task LaunchAsync_StopsOnMandatoryTaskFailure()
        {
            // Arrange
            var mandatoryTaskMock = new Mock<ILaunchTask>();
            mandatoryTaskMock.Setup(t => t.Name).Returns("MandatoryTask");
            mandatoryTaskMock.Setup(t => t.IsOptional).Returns(false);
            mandatoryTaskMock.Setup(t => t.ExecuteAsync(It.IsAny<LaunchRequest>(), It.IsAny<IProgress<StepProgress>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new StepProgress("MandatoryTask", ProgressType.StepCompleted, "Failed", null, TimeSpan.Zero, false, "Error"));

            _appsManagerMock.Setup(m => m.GetTasks(RunOption.BeforeSimStarts)).Returns(new[] { mandatoryTaskMock.Object });

            var manager = CreateManager();

            // Act
            var result = await manager.LaunchAsync(new LaunchRequest(), null, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual("Error", result.ErrorMessage);
        }
    }
}