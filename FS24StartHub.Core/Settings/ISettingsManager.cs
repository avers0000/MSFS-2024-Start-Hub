using FS24StartHub.Core.Domain;

namespace FS24StartHub.Core.Settings
{
    public interface ISettingsManager
    {
        AppSettings Load();
        void Update(AppSettings settings);
        bool ValidateSimConfiguration(AppSettings settings);
    }
}
