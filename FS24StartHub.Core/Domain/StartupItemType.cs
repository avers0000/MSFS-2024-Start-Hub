using System.ComponentModel;

namespace FS24StartHub.Core.Domain
{
    /// <summary>
    /// Defines the type of startup item.
    /// </summary>
    public enum StartupItemType
    {
        [Description("Application")]
        App = 0,

        [Description("Script")]
        Script = 1
    }
}
