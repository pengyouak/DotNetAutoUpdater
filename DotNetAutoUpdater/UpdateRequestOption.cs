using System.Net;

namespace DotNetAutoUpdater
{
    public class UpdateRequestOption
    {
        public NetworkCredential FtpCredentials;

        public string HttpUserAgent;

        public IWebProxy Proxy;

        public IAuthentication RequestAuthorization;
    }
}