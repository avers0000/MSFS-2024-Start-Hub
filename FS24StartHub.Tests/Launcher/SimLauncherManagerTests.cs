using FS24StartHub.Core.Apps;
using FS24StartHub.Core.Domain;
using FS24StartHub.Core.Launcher;
using FS24StartHub.Core.Launcher.Progress;
using FS24StartHub.Core.Launcher.Tasks;
using FS24StartHub.Core.Logging;
using FS24StartHub.Core.Settings;
using FS24StartHub.Infrastructure.Helpers;
using FS24StartHub.Infrastructure.Launcher;
using FS24StartHub.Infrastructure.Logging;
using Moq;
using System.Diagnostics;

namespace FS24StartHub.Tests.Launcher
{
    [TestClass]
    public class SimLauncherManagerTests
    {
        [TestMethod]
        public async Task LaunchAsync_CancelledBeforeStart_ThrowsOperationCanceled()
        {
            // Arrange
            var sinkMock = new Mock<ILogSink>();
            var logManager = new LogManager(new[] { sinkMock.Object });

            var settingsManagerMock = new Mock<ISettingsManager>();
            settingsManagerMock.Setup(s => s.CurrentSettings).Returns(new AppSettings
            {
                SimType = SimType.Custom,
                SimExePath = "dummy.exe",
                LaunchTimeoutSeconds = 1
            });

            var appsManagerMock = new Mock<IAppsManager>();
            appsManagerMock.Setup(m => m.GetTasks(It.IsAny<RunOption>())).Returns(Enumerable.Empty<ILaunchTask>());

            var manager = new SimLauncherManager(logManager, settingsManagerMock.Object, appsManagerMock.Object);
            var request = new LaunchRequest();

            using var cts = new CancellationTokenSource();
            cts.Cancel(); // token is already cancelled

            // Act + Assert
            await Assert.ThrowsExceptionAsync<OperationCanceledException>(
                () => manager.LaunchAsync(request, null, cts.Token));
        }

        [TestMethod]
        public async Task LaunchAsync_CancelledDuringExecution_ThrowsOperationCanceled()
        {
            // Arrange
            var sinkMock = new Mock<ILogSink>();
            var logManager = new LogManager(new[] { sinkMock.Object });

            var settingsManagerMock = new Mock<ISettingsManager>();
            var appsManagerMock = new Mock<IAppsManager>();

            // Mock GetSaveTask to return a valid task
            var saveTaskMock = new Mock<ILaunchTask>();
            saveTaskMock.Setup(t => t.Name).Returns("SaveTask");
            saveTaskMock.Setup(t => t.IsOptional).Returns(true);
            saveTaskMock.Setup(t => t.ExecuteAsync(It.IsAny<LaunchRequest>(), It.IsAny<IProgress<StepProgress>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new StepProgress("SaveTask", ProgressType.StepCompleted, "Completed", null, TimeSpan.Zero, true, null));
            appsManagerMock.Setup(m => m.GetSaveTask()).Returns(saveTaskMock.Object);

            // Mock GetTasks to return a valid list of tasks
            var longRunningTaskMock = new Mock<ILaunchTask>();
            longRunningTaskMock.Setup(t => t.Name).Returns("LongRunningTask");
            longRunningTaskMock.Setup(t => t.IsOptional).Returns(false);
            longRunningTaskMock.Setup(t => t.ExecuteAsync(It.IsAny<LaunchRequest>(), It.IsAny<IProgress<StepProgress>>(), It.IsAny<CancellationToken>()))
                .Returns(async (LaunchRequest request, IProgress<StepProgress> progress, CancellationToken ct) =>
                {
                    await Task.Delay(5000, ct); // Simulate long-running task with cancellation support
                    return new StepProgress("LongRunningTask", ProgressType.StepCompleted, "Completed", null, TimeSpan.Zero, true, null);
                });

            appsManagerMock.Setup(m => m.GetTasks(RunOption.BeforeSimStarts)).Returns(new[] { longRunningTaskMock.Object });
            appsManagerMock.Setup(m => m.GetTasks(RunOption.AfterSimStarts)).Returns(Enumerable.Empty<ILaunchTask>());

            var manager = new SimLauncherManager(logManager, settingsManagerMock.Object, appsManagerMock.Object);

            var cts = new CancellationTokenSource();
            cts.CancelAfter(100); // Cancel after 100ms

            // Act + Assert
            try
            {
                await manager.LaunchAsync(new LaunchRequest(), null, cts.Token);
                Assert.Fail("Expected an OperationCanceledException to be thrown.");
            }
            catch (OperationCanceledException)
            {
                // Test passes if OperationCanceledException or TaskCanceledException is thrown
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public async Task LaunchAsync_StopsOnMandatoryTaskFailure()
        {
            // Arrange
            var logManagerMock = new Mock<ILogManager>();
            var settingsManagerMock = new Mock<ISettingsManager>();
            var appsManagerMock = new Mock<IAppsManager>();

            var mandatoryTaskMock = new Mock<ILaunchTask>();
            mandatoryTaskMock.Setup(t => t.Name).Returns("MandatoryTask");
            mandatoryTaskMock.Setup(t => t.IsOptional).Returns(false);
            mandatoryTaskMock.Setup(t => t.ExecuteAsync(It.IsAny<LaunchRequest>(), It.IsAny<IProgress<StepProgress>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new StepProgress("MandatoryTask", ProgressType.StepCompleted, "Failed", null, TimeSpan.Zero, false, "Error"));

            appsManagerMock.Setup(m => m.GetTasks(RunOption.BeforeSimStarts)).Returns(new[] { mandatoryTaskMock.Object });

            var saveTaskMock = new Mock<ILaunchTask>();
            saveTaskMock.Setup(t => t.Name).Returns("SaveTask");
            saveTaskMock.Setup(t => t.IsOptional).Returns(true);
            saveTaskMock.Setup(t => t.ExecuteAsync(It.IsAny<LaunchRequest>(), It.IsAny<IProgress<StepProgress>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new StepProgress("SaveTask", ProgressType.StepCompleted, "Completed", null, TimeSpan.Zero, true, null));
            appsManagerMock.Setup(m => m.GetSaveTask()).Returns(saveTaskMock.Object);

            var manager = new SimLauncherManager(logManagerMock.Object, settingsManagerMock.Object, appsManagerMock.Object);

            // Act
            var result = await manager.LaunchAsync(new LaunchRequest(), null, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.Success, "The pipeline should stop on mandatory task failure.");
            Assert.AreEqual("Error", result.ErrorMessage, "The error message is incorrect.");
        }
    }
}
