using System;
using System.Diagnostics;
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

        public IUpdateOptionProvider UpdateOptionProvider;

        public IUpdateStartInfoProvider UpdateStartInfoProvider;

        #endregion fields

        #region properties

        public AppUpdateArgs AppUpdateArgs { get; set; }

        public bool Synchronous { get; set; }

        public Uri UpdateUri { get; set; }

        public UpdateOption UpdateOption { get; set; }

        #endregion properties

        #region constructors

        public UpdateContext()
        {
            UpdateOptionProvider = new XmlUpdateOptionProvider();
            UpdateStartInfoProvider = new DefaultUpdateStartInfoProvider();

            var p = Process.GetCurrentProcess();
            AppUpdateArgs = new AppUpdateArgs(p.Id, p.ProcessName, p.MainModule.FileName);
        }

        #endregion constructors
    }
}