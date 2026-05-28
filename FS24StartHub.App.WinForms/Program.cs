using FS24StartHub.Core.Apps;
using FS24StartHub.Core.Configs;
using FS24StartHub.Core.Domain;
using FS24StartHub.Core.Logging;
using FS24StartHub.Core.Settings;
using FS24StartHub.Core.Storage;
using FS24StartHub.Infrastructure.Apps;
using FS24StartHub.Infrastructure.Configs;
using FS24StartHub.Infrastructure.Helpers;
using FS24StartHub.Infrastructure.Logging;
using FS24StartHub.Infrastructure.Settings;
using FS24StartHub.Infrastructure.Storage;

namespace FS24StartHub.App.WinForms
{
    internal static class Program
    {
        private static Mutex? appMutex;

        [STAThread]
        static void Main()
        {
            const string mutexName = "FS24StartHub.App.WinForms.UniqueInstance";

            bool createdNew;
            appMutex = new Mutex(true, mutexName, out createdNew);

            if (!createdNew)
            {
                MessageBox.Show(
                    "FS24StartHub is already running.",
                    "FS24StartHub",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            ApplicationConfiguration.Initialize();

            string baseFolderPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "FS24StartHub");

            IFileStorage fileStorage = new FileStorage();
            IJsonStorage jsonStorage = new JsonStorage(fileStorage);

            ILogSink fileSink = new JsonFileLogSink(fileStorage, baseFolderPath);

            var sinks = new List<ILogSink> { fileSink };
#if DEBUG
            sinks.Add(new ConsoleLogSink());
#endif

            ILogManager logManager = new LogManager(sinks);

            if (Utility.IsSimulatorRunning())
            {
                logManager.Warn("Simulator already running. Application aborted.", "Program", "SimulatorAlreadyRunning");
                MessageBox.Show(
                    "Microsoft Flight Simulator 2024 is already running.\nPlease close it before starting FS24StartHub.",
                    "FS24StartHub",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            logManager.Info("Application starting...", "Program", "Startup");

            var settingsManager = new SettingsManager(baseFolderPath, fileStorage, jsonStorage, logManager);

            var firstRun = new FirstRunInitializer(fileStorage, jsonStorage, logManager, baseFolderPath);
            bool initialized;
            try
            {
                initialized = firstRun.Initialize();
            }
            catch (IOException ex)
            {
                logManager.Error("Failed to initialize configuration", "Program", ex);
                MessageBox.Show(
                    "Could not initialize configuration.\nCheck file system permissions and restart.",
                    "FS24StartHub",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (!initialized && !ShowSettingsForm(settingsManager, logManager)) return;

            AppSettings settings;
            try
            {
                settings = settingsManager.Load();
            }
            catch (FileNotFoundException ex)
            {
                logManager.Error("Settings file missing", "Program", ex);
                MessageBox.Show(
                    "Settings file not found.\nFS24StartHub cannot start without configuration.",
                    "FS24StartHub",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            catch (InvalidDataException ex)
            {
                logManager.Error("Settings file corrupted", "Program", ex);
                MessageBox.Show(
                    "Settings file is corrupted.\nPlease fix fs24sh.json manually.",
                    "FS24StartHub",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (!settingsManager.ValidateSimConfiguration(settings))
            {
                logManager.Warn("Simulator configuration is invalid.", "Program", "InvalidSimConfig");

                if (!ShowSettingsForm(settingsManager, logManager))
                    return;

                try
                {
                    settings = settingsManager.Load();
                }
                catch
                {
                    return;
                }
            }

            // Initialize AppsManager
            IAppsManager appsManager = new AppsManager(settingsManager, logManager);
            IConfigManager configManager = new ConfigManager(settingsManager, fileStorage, logManager);
            // Run the main form
            Application.Run(new MainForm(settingsManager, appsManager, configManager, logManager));
        }

        private static bool ShowSettingsForm(ISettingsManager settingsManager, ILogManager logManager)
        {
            using var settingsForm = new SettingsForm(settingsManager, logManager, firstRunMode: true);
            return settingsForm.ShowDialog() == DialogResult.OK;
        }
    }
}