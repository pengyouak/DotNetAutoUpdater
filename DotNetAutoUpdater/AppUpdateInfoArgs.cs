using System;
using System.IO;

namespace DotNetAutoUpdater
{
    public class AppUpdateInfoArgs
    {
        public string TempFolderPath { get; set; } = Path.Combine(Path.GetTempPath(), "DotNetAutoUpdater");

        public string DownloadFolderName { get; set; } = "_Update";

        public string BackupFolderName { get; set; } = "_Backup";

        public string InstallFolderPath { get; set; } = AppDomain.CurrentDomain.BaseDirectory;

        public string TempUpdateOption { get; set; } = "UpdateOption.tmp";

        public string UpdateToolName { get; set; } = "DotNetAutoUpdater.exe";

        public string APPFullName { get; set; }

        public string AppName { get; set; }

        public int PID { get; set; }

        #region public methods

        public string GetDownloadFolderFullPath() => Path.Combine(TempFolderPath, DownloadFolderName);

        public string GetBackupFolderFullPath() => Path.Combine(TempFolderPath, BackupFolderName);

        #endregion public methods
    }
}