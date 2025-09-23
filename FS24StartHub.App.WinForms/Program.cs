using FS24StartHub.Core.Domain;
using FS24StartHub.Core.Logging;
using FS24StartHub.Core.Storage;
using FS24StartHub.Infrastructure.Helpers;
using FS24StartHub.Infrastructure.Logging;
using FS24StartHub.Infrastructure.Settings;
using FS24StartHub.Infrastructure.Storage;
using System.Collections.Generic;

namespace FS24StartHub.App.WinForms
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
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

            if (!initialized)
            {
                MessageBox.Show(
                    "Simulator not found.\nUpdate configuration manually in fs24sh.json and restart application.",
                    "FS24StartHub",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            var settingsManager = new SettingsManager(baseFolderPath, fileStorage, jsonStorage, logManager);
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

                MessageBox.Show(
                    "Simulator configuration is invalid.\nYou can manually re-run simulator detection later.\nCheck fs24sh.json or use the upcoming recovery feature.",
                    "FS24StartHub",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            Application.Run(new MainForm(settingsManager));
        }
    }
}