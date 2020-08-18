namespace DotNetAutoUpdater
{
    public class ParseStartInfoArg
    {
        public string DownloadFolderPath { get; set; }

        public string BackupFolderPath { get; set; }

        public string InstallFolderPath { get; set; }

        public string TempUpdateOption { get; set; }

        public string UpdateToolName { get; set; }

        public string[] CommandLineArgs { get; set; }
    }
}