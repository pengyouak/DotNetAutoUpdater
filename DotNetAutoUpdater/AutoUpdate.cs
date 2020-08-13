using System;
using System.Net;
using System.Net.Cache;
using System.Reflection;

namespace DotNetAutoUpdater
{
    public class AutoUpdate : IAutoUpdater
    {
        private UpdateOption _updateOption;

        public delegate void CheckForUpdateEventHandler(AutoUpdateArgs args);

        public event CheckForUpdateEventHandler CheckForUpdateEvent;

        public UpdateRequestOption UpdateRequestOption { get; set; }

        public void Update(string url)
        {
            UpdateRequestOption = new UpdateRequestOption();

            var uri = new Uri(url);

            BindOption(uri);

            if (_updateOption == null) return;

            if (_updateOption.UpdateMode == UpdateMode.Force)
                StartUpdate();
        }

        private void BindOption(Uri uri)
        {
            WebClient webClient = new WebClient
            {
                CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore)
            };

            if (UpdateRequestOption.Proxy != null)
            {
                webClient.Proxy = UpdateRequestOption.Proxy;
            }

            if (uri.Scheme.Equals(Uri.UriSchemeFtp))
            {
                webClient.Credentials = UpdateRequestOption.FtpCredentials;
            }
            else
            {
                if (UpdateRequestOption.RequestAuthorization != null)
                {
                    webClient.Headers[HttpRequestHeader.Authorization] = UpdateRequestOption.RequestAuthorization.ToString();
                }

                webClient.Headers[HttpRequestHeader.UserAgent] = UpdateRequestOption.HttpUserAgent;
            }

            var jsonFile = webClient.DownloadString(uri);

            if (string.IsNullOrEmpty(jsonFile))
            {
                CheckForUpdateEvent?.Invoke(new AutoUpdateArgs
                {
                    Uri = uri,
                    Message = ConstResources.UpdateJsonFileEmpty,
                    UpdateRequestOption = this.UpdateRequestOption
                });
                return;
            }

            try
            {
                _updateOption = Newtonsoft.Json.JsonConvert.DeserializeObject<UpdateOption>(jsonFile);

                _updateOption.InstalledVersion = Assembly.GetEntryAssembly().GetName().Version;

                _updateOption.IsUpdateAvailable = _updateOption.UpdateVersion > _updateOption.InstalledVersion;
            }
            catch
            {
                CheckForUpdateEvent?.Invoke(new AutoUpdateArgs
                {
                    Uri = uri,
                    Message = ConstResources.UpdateJsonFileEmpty,
                    UpdateRequestOption = this.UpdateRequestOption
                });
                return;
            }
        }

        private void StartUpdate()
        {
        }
    }
}