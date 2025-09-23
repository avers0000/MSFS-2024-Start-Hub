namespace FS24StartHub.Core.Launcher.Progress
{
    public sealed class StepProgress
    {
        public string StepName { get; }
        public ProgressType Type { get; }
        public string Message { get; }
        public int? Percent { get; }

        // поля для Completed
        public TimeSpan? Duration { get; }
        public bool Success { get; }
        public string? Error { get; }

        public StepProgress(
            string stepName,
            ProgressType type,
            string message,
            int? percent = null,
            TimeSpan? duration = null,
            bool success = false,
            string? error = null)
        {
            StepName = stepName;
            Type = type;
            Message = message;
            Percent = percent;
            Duration = duration;
            Success = success;
            Error = error;
        }
    }
}