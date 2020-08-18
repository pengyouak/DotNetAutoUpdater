using System;
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

        public AppUpdateInfoArgs AppUpdateInfoArgs { get; set; }

        public bool Synchronous { get; set; }

        public Uri UpdateUri { get; set; }

        public UpdateOption UpdateOption { get; set; }

        #endregion properties

        #region constructors

        public UpdateContext()
        {
            UpdateOptionProvider = new XmlUpdateOptionProvider();
            UpdateStartInfoProvider = new DefaultUpdateStartInfoProvider();

            AppUpdateInfoArgs = new AppUpdateInfoArgs();
        }

        #endregion constructors
    }
}