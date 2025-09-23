using System.Text.Json;
using System.Text.Json.Serialization;

namespace FS24StartHub.Core.Logging
{
    public static class LogJsonDefaults
    {
        public static readonly JsonSerializerOptions Options = new()
        {
            Converters = { new JsonStringEnumConverter() }
        };
    }
}