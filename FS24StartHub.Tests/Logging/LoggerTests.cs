using FS24StartHub.Core.Logging;
using FS24StartHub.Infrastructure.Logging;
using Moq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FS24StartHub.Tests.Logging
{
    [TestClass]
    public class LogManagerTests
    {
        private static LogEvent? Deserialize(string json) =>
        JsonSerializer.Deserialize<LogEvent>(json, LogJsonDefaults.Options);


        private static bool IsInfoEvent(string json, string expectedMessage, string expectedModule)
        {
            var evt = Deserialize(json);
            return evt != null
                && evt.Level == LogLevel.Info
                && evt.Message == expectedMessage
                && evt.Module == expectedModule;
        }

        private static bool IsWarnEvent(string json, string expectedMessage, string expectedModule)
        {
            var evt = Deserialize(json);
            return evt != null
                && evt.Level == LogLevel.Warn
                && evt.Message == expectedMessage
                && evt.Module == expectedModule;
        }

        private static bool IsErrorEvent(string json, string expectedMessage, string expectedModule)
        {
            var evt = Deserialize(json);
            return evt != null
                && evt.Level == LogLevel.Error
                && evt.Message.Contains(expectedMessage)
                && evt.Module == expectedModule;
        }

        private static bool IsDebugEvent(string json, string expectedMessage, string expectedModule)
        {
            var evt = Deserialize(json);
            return evt != null
                && evt.Level == LogLevel.Debug
                && evt.Message == expectedMessage
                && evt.Module == expectedModule;
        }

        private static bool IsFatalEvent(string json, string expectedMessage, string expectedModule)
        {
            var evt = Deserialize(json);
            return evt != null
                && evt.Level == LogLevel.Fatal
                && evt.Message == expectedMessage
                && evt.Module == expectedModule;
        }

        [TestMethod]
        public void Info_ShouldWriteLogEvent_WithInfoLevel()
        {
            var sink = new Mock<ILogSink>();
            var manager = new LogManager(new[] { sink.Object });

            manager.Info("Test info", "TestModule");

            sink.Verify(s => s.Write(It.Is<string>(json =>
                IsInfoEvent(json, "Test info", "TestModule")
            )), Times.Once);
        }

        [TestMethod]
        public void Warn_ShouldWriteLogEvent_WithWarnLevel()
        {
            var sink = new Mock<ILogSink>();
            var manager = new LogManager(new[] { sink.Object });

            manager.Warn("Test warning", "TestModule");

            sink.Verify(s => s.Write(It.Is<string>(json =>
                IsWarnEvent(json, "Test warning", "TestModule")
            )), Times.Once);
        }

        [TestMethod]
        public void Error_ShouldWriteLogEvent_WithErrorLevel()
        {
            var sink = new Mock<ILogSink>();
            var manager = new LogManager(new[] { sink.Object });

            manager.Error("Test error", "TestModule");

            sink.Verify(s => s.Write(It.Is<string>(json =>
                IsErrorEvent(json, "Test error", "TestModule")
            )), Times.Once);
        }

        [TestMethod]
        public void Debug_ShouldWriteLogEvent_WithDebugLevel()
        {
            var sink = new Mock<ILogSink>();
            var manager = new LogManager(new[] { sink.Object });

            manager.Debug("Debug details", "TestModule");

            sink.Verify(s => s.Write(It.Is<string>(json =>
                IsDebugEvent(json, "Debug details", "TestModule")
            )), Times.Once);
        }

        [TestMethod]
        public void Fatal_ShouldWriteLogEvent_WithFatalLevel()
        {
            var sink = new Mock<ILogSink>();
            var manager = new LogManager(new[] { sink.Object });

            manager.Fatal("Fatal crash", "TestModule");

            sink.Verify(s => s.Write(It.Is<string>(json =>
                IsFatalEvent(json, "Fatal crash", "TestModule")
            )), Times.Once);
        }

        [TestMethod]
        public void LogManager_ShouldSendMessages_ToAllSinks()
        {
            var sink1 = new Mock<ILogSink>();
            var sink2 = new Mock<ILogSink>();
            var manager = new LogManager(new[] { sink1.Object, sink2.Object });

            manager.Info("Broadcast", "TestModule");

            sink1.Verify(s => s.Write(It.Is<string>(json =>
                IsInfoEvent(json, "Broadcast", "TestModule")
            )), Times.Once);

            sink2.Verify(s => s.Write(It.Is<string>(json =>
                IsInfoEvent(json, "Broadcast", "TestModule")
            )), Times.Once);
        }
    }
}