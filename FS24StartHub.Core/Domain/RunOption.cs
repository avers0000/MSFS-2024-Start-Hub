using System.ComponentModel;

namespace FS24StartHub.Core.Domain
{
    /// <summary>
    /// Defines execution timing for startup items.
    /// </summary>
    public enum RunOption
    {
        [Description("Before Flight Simulator Starts")]
        BeforeSimStarts = 0,
        [Description("After Flight Simulator Starts")]
        AfterSimStarts = 1
        //[Description("After Flight Simulator Ends")]
        //AfterSimEnds = 2
    }
}
