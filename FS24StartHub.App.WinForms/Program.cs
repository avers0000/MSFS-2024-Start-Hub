using FS24StartHub.Core.Logging;
using FS24StartHub.Core.Storage;
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

            logger.Info("Application starting...");

            // 4. Create storage services
            IFileStorage fileStorage = new FileStorage();
            IJsonStorage jsonStorage = new JsonStorage(fileStorage);

            // 5. First run initialization
            var firstRun = new FirstRunInitializer(fileStorage, jsonStorage, logger, baseFolderPath);
            if (!firstRun.Initialize())
            {
                MessageBox.Show(
                    "Simulator not found.\nUpdate configuration manually in fs24sh.json and restart application.",
                    "FS24StartHub",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                Application.Exit();
                return;
            }

            // 6. TODO: SettingsManager (load settings here later)
            var settingsManager = new SettingsManager(baseFolderPath, fileStorage, jsonStorage, logger);
            var settings = settingsManager.Load();

            // 7. Start UI
            Application.Run(new MainForm());
        }
    }
}