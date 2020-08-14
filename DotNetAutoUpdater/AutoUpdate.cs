using DotNetAutoUpdater.Properties;
using DotNetAutoUpdater.UpdateDialogs;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Reflection;
using System.Text;

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

            try
            {
                var xmlFile = webClient.DownloadString(uri);

                if (string.IsNullOrEmpty(xmlFile))
                {
                    CheckForUpdateEvent?.Invoke(new AutoUpdateArgs
                    {
                        Uri = uri,
                        Message = ConstResources.UpdateJsonFileEmpty,
                        UpdateRequestOption = this.UpdateRequestOption
                    });
                    return false;
                }
                _updateOption = UpdateOption.LoadUpdateOption(xmlFile);

                _updateOption.InstalledVersion = Assembly.GetEntryAssembly().GetName().Version;

                _updateOption.IsUpdateAvailable = _updateOption.UpdateVersion > _updateOption.InstalledVersion;
            }
            catch (WebException)
            {
                CheckForUpdateEvent?.Invoke(new AutoUpdateArgs
                {
                    Uri = uri,
                    Message = ConstResources.UpdateJsonFileNotFound,
                    UpdateRequestOption = this.UpdateRequestOption
                });
                return false;
            }
            catch (Exception)
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
                if (confirm.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;
            }

            var download = new DownloadDiaglog(_updateOption);
            if (download.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;

            ExecUpdateApp();
        }

        private void ExecUpdateApp()
        {
            var updaterExe = Path.Combine(ConstResources.TempFolder, "DotNetAutoUpdater.exe");
            File.WriteAllBytes(updaterExe, Resources.DotNetAutoUpdater_App);

            var arguments = new StringBuilder();
            arguments.Append($"/pid {Process.GetCurrentProcess().Id} ");
            arguments.Append($"/app {System.Windows.Forms.Application.ExecutablePath} ");

            var processInfo = new ProcessStartInfo(updaterExe, arguments.ToString())
            {
                UseShellExecute = true
            };
            Process.Start(processInfo);
        }
    }
}