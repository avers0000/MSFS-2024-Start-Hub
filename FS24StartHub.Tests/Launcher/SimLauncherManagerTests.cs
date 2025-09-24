using FS24StartHub.Core.Domain;
using FS24StartHub.Core.Launcher;
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

            var manager = new SimLauncherManager(logManager, settingsManagerMock.Object);
            var request = new LaunchRequest();

            using var cts = new CancellationTokenSource();
            cts.Cancel(); // token is already cancelled

            // Act + Assert
            await Assert.ThrowsExceptionAsync<OperationCanceledException>(
                () => manager.LaunchAsync(request, null, cts.Token));
        }
    }
}
