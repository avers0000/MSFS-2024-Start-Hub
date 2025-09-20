using FS24StartHub.Core.Logging;           // ILogSink
using FS24StartHub.Infrastructure.Logging; // Logger
using Moq;

namespace FS24StartHub.Tests.Logging
{
    [TestClass]
    public class LoggerTests
    {
        [TestMethod]
        public void Info_ShouldCallWrite_WithInfoLevel()
        {
            // Arrange
            var sink = new Mock<ILogSink>();
            var logger = new Logger(new ILogSink[] { sink.Object });

            // Act
            logger.Info("Test info");

            // Assert
            sink.Verify(s => s.Write("INFO", "Test info"), Times.Once);
        }

        [TestMethod]
        public void Warn_ShouldCallWrite_WithWarnLevel()
        {
            // Arrange
            var sink = new Mock<ILogSink>();
            var logger = new Logger(new ILogSink[] { sink.Object });

            // Act
            logger.Warn("Test warning");

            // Assert
            sink.Verify(s => s.Write("WARN", "Test warning"), Times.Once);
        }

        [TestMethod]
        public void Error_ShouldCallWrite_WithErrorLevel()
        {
            // Arrange
            var sink = new Mock<ILogSink>();
            var logger = new Logger(new ILogSink[] { sink.Object });

            // Act
            logger.Error("Test error");

            // Assert
            sink.Verify(s => s.Write("ERROR", "Test error"), Times.Once);
        }

        [TestMethod]
        public void Logger_ShouldSendMessages_ToAllSinks()
        {
            // Arrange
            var sink1 = new Mock<ILogSink>();
            var sink2 = new Mock<ILogSink>();
            var logger = new Logger(new ILogSink[] { sink1.Object, sink2.Object });

            // Act
            logger.Info("Broadcast");

            // Assert
            sink1.Verify(s => s.Write("INFO", "Broadcast"), Times.Once);
            sink2.Verify(s => s.Write("INFO", "Broadcast"), Times.Once);
        }
    }
}