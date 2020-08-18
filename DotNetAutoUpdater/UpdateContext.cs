using System;
using System.IO;
using System.Net;

namespace DotNetAutoUpdater
{
    public class UpdateContext
    {
        #region fields

        public NetworkCredential FtpCredentials;

        public string HttpUserAgent;

        public IWebProxy Proxy;

        public IAuthentication RequestAuthorization;

        public IUpdateOptionHandler UpdateOptionHandler;

        public IUpdateStartInfoHandler UpdateStartInfoHandler;

        #endregion fields

        #region properties

        public string TempFolderPath { get; set; } = Path.Combine(Path.GetTempPath(), "DotNetAutoUpdater");

        public string DownloadFolderName { get; set; } = "_Update";

        public string BackupFolderName { get; set; } = "_Backup";

        public string InstallFolderPath { get; set; } = AppDomain.CurrentDomain.BaseDirectory;

        public string TempUpdateOption { get; set; } = "UpdateOption.tmp";

        public string UpdateToolName { get; set; } = "DotNetAutoUpdater.exe";

        public string APPFullName { get; set; }

        public bool Synchronous { get; set; }

        public Uri UpdateUri { get; set; }

        public UpdateOption UpdateOption { get; set; }

        #endregion properties

        #region public methods

        public string GetDownloadFolderFullPath() => Path.Combine(TempFolderPath, DownloadFolderName);

        public string GetBackupFolderFullPath() => Path.Combine(TempFolderPath, BackupFolderName);

        #endregion public methods

        #region constructors

        public UpdateContext()
        {
            UpdateOptionHandler = new XmlUpdateOptionHandler();
            UpdateStartInfoHandler = new DefaultUpdateStartInfoHandler();
        }

        #endregion constructors
    }
}