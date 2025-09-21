using FS24StartHub.Core.Domain;
using FS24StartHub.Core.Logging;
using FS24StartHub.Core.Storage;
using FS24StartHub.Infrastructure.Helpers;
using FS24StartHub.Infrastructure.Logging;
using FS24StartHub.Infrastructure.Settings;
using FS24StartHub.Infrastructure.Storage;

namespace FS24StartHub.App.WinForms
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            // 1. Initialize WinForms configuration (DPI, fonts, etc.)
            ApplicationConfiguration.Initialize();

            // 2. Resolve base folder for application data
            string baseFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "FS24StartHub");

            // 3. Create logger (FileLogSink сам создаёт Logs и файл)
            ILogSink fileSink = new FileLogSink(baseFolderPath);
            ILogSink consoleSink = new ConsoleLogSink();

            ILogger logger = new Logger([fileSink, consoleSink]);

            if (Utility.IsSimulatorRunning())
            {
                logger.Warn("Simulator already running. Application aborted.");

                MessageBox.Show(
                    "Microsoft Flight Simulator 2024 is already running.\nPlease close it before starting FS24StartHub.",
                    "FS24StartHub",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            logger.Info("Application starting...");

            // 4. Create storage services
            IFileStorage fileStorage = new FileStorage();
            IJsonStorage jsonStorage = new JsonStorage(fileStorage);

            // 5. First run initialization
            var firstRun = new FirstRunInitializer(fileStorage, jsonStorage, logger, baseFolderPath);
            bool initialized;
            try
            {
                initialized = firstRun.Initialize();
            }
            catch (IOException ex)
            {
                logger.Error("Failed to initialize configuration: " + ex.Message);
                MessageBox.Show(
                    "Could not initialize configuration.\nCheck file system permissions and restart.",
                    "FS24StartHub",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (!initialized)
            {
                MessageBox.Show(
                    "Simulator not found.\nUpdate configuration manually in fs24sh.json and restart application.",
                    "FS24StartHub",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            // 6. TODO: SettingsManager (load settings here later)
            var settingsManager = new SettingsManager(baseFolderPath, fileStorage, jsonStorage, logger);
            AppSettings settings;
            try
            {
                settings = settingsManager.Load();
            }
            catch (FileNotFoundException ex)
            {
                logger.Error("Settings file missing: " + ex.FileName);
                MessageBox.Show(
                    "Settings file not found.\nFS24StartHub cannot start without configuration.",
                    "FS24StartHub",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            catch (InvalidDataException ex)
            {
                logger.Error("Settings file corrupted: " + ex.Message);
                MessageBox.Show(
                    "Settings file is corrupted.\nPlease fix fs24sh.json manually.",
                    "FS24StartHub",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (!settingsManager.ValidateSimConfiguration(settings))
            {
                logger.Warn("Simulator configuration is invalid.");
                MessageBox.Show(
                    "Simulator configuration is invalid.\nYou can manually re-run simulator detection later.\nCheck fs24sh.json or use the upcoming recovery feature.",
                    "FS24StartHub",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            // 7. Start UI
            Application.Run(new MainForm(settingsManager));
        }
    }
}