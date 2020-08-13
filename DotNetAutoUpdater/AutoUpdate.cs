using DotNetAutoUpdater.UpdateDialogs;
using System;
using System.Net;
using System.Net.Cache;
using System.Reflection;

namespace DotNetAutoUpdater
{
    public class AutoUpdate : IAutoUpdater
    {
        #region private fields

        private UpdateOption _updateOption;

        #endregion private fields

        #region public events

        public delegate void CheckForUpdateEventHandler(AutoUpdateArgs args);

        public event CheckForUpdateEventHandler CheckForUpdateEvent;

        #endregion public events

        #region public properties

        public UpdateRequestOption UpdateRequestOption { get; set; }

        #endregion public properties

        public void Update(UpdateOption updateOption)
        {
            if (updateOption == null) return;

            _updateOption = updateOption;

            _updateOption.InstalledVersion = Assembly.GetEntryAssembly().GetName().Version;

            _updateOption.IsUpdateAvailable = _updateOption.UpdateVersion > _updateOption.InstalledVersion;

            StartUpdate();
        }

        public void Update(string url)
        {
            UpdateRequestOption = new UpdateRequestOption();

            var uri = new Uri(url);

            if (!BindOption(uri)) return;

            StartUpdate();
        }

        private bool BindOption(Uri uri)
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
                return false;
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
                return false;
            }
            return true;
        }

        private void StartUpdate()
        {
            if (!_updateOption.IsUpdateAvailable) return;

            if (_updateOption.UpdateMode != UpdateMode.Force)
            {
                var confirm = new ConfirmDiaglog(_updateOption);
                confirm.ShowDialog();
            }
        }
    }
}