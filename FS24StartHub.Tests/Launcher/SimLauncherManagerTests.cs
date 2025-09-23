using FS24StartHub.Core.Launcher;
using FS24StartHub.Core.Logging;
using FS24StartHub.Infrastructure.Launcher;
using FS24StartHub.Infrastructure.Logging;
using Moq;

namespace FS24StartHub.Tests.Launcher
{
    [TestClass]
    public class SimLauncherManagerTests
    {
        [TestMethod]
        public async Task LaunchAsync_ReturnsSuccessResult()
        {
            // Arrange
            var sinkMock = new Mock<ILogSink>();
            var logManager = new LogManager(new[] { sinkMock.Object });

            var manager = new SimLauncherManager(logManager);
            var request = new LaunchRequest();

            // Act
            var result = await manager.LaunchAsync(request, null, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success, "LaunchResult should indicate success");
            Assert.AreEqual(ExitIntent.None, result.ExitIntent, "ExitIntent should be None");
            Assert.IsTrue(result.Steps.Count > 0, "LaunchResult should contain steps");
            Assert.IsTrue(result.Steps.All(s => s.Success), "All steps should be successful");

            // Optional
            sinkMock.Verify(s => s.Write(It.IsAny<string>()), Times.AtLeastOnce);
        }
    }
}