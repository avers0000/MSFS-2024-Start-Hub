namespace FS24StartHub.Core.Domain
{
    /// <summary>
    /// Defines execution timing for startup items.
    /// </summary>
    public enum RunOption
    {
        BeforeSimStarts = 0,
        AfterSimStarts = 1,
        AfterSimEnds = 2
    }
}
