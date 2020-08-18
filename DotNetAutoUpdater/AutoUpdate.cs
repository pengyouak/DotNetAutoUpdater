using DotNetAutoUpdater.UpdateDialogs;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Cache;
using System.Reflection;

namespace DotNetAutoUpdater
{
    public class AutoUpdate : IAutoUpdater
    {
        #region public events

        public delegate void CheckForUpdateEventHandler(AutoUpdateArgs args);

        public event CheckForUpdateEventHandler CheckForUpdateEvent;

        #endregion public events

        #region public properties

        public UpdateContext UpdateContext { get; private set; }

        #endregion public properties

        public void Update(UpdateOption updateOption)
        {
            UpdateContext = new UpdateContext();

            if (updateOption == null) return;

            UpdateContext.UpdateOption = updateOption;

            UpdateContext.UpdateOption.InstalledVersion = Assembly.GetEntryAssembly().GetName().Version;

            UpdateContext.UpdateOption.IsUpdateAvailable = UpdateContext.UpdateOption.UpdateVersion > UpdateContext.UpdateOption.InstalledVersion;

            StartUpdate();
        }

        public void Update(string url)
        {
            UpdateContext = new UpdateContext();

            UpdateContext.UpdateUri = new Uri(url);

            if (!BindOption(UpdateContext.UpdateUri)) return;

            StartUpdate();
        }

        private bool BindOption(Uri uri)
        {
            WebClient webClient = new WebClient
            {
                CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore)
            };

            if (UpdateContext.Proxy != null)
            {
                webClient.Proxy = UpdateContext.Proxy;
            }

            if (uri.Scheme.Equals(Uri.UriSchemeFtp))
            {
                webClient.Credentials = UpdateContext.FtpCredentials;
            }
            else
            {
                if (UpdateContext.RequestAuthorization != null)
                {
                    webClient.Headers[HttpRequestHeader.Authorization] = UpdateContext.RequestAuthorization.ToString();
                }

                webClient.Headers[HttpRequestHeader.UserAgent] = UpdateContext.HttpUserAgent;
            }

            try
            {
                var xmlFile = webClient.DownloadString(uri);

                if (string.IsNullOrEmpty(xmlFile))
                {
                    CheckForUpdateEvent?.Invoke(new AutoUpdateArgs
                    {
                        Message = ConstResources.UpdateXmlFileEmpty,
                        UpdateContext = UpdateContext
                    });
                    return false;
                }

                if (UpdateContext.UpdateOptionProvider == null) return false;

                UpdateContext.UpdateOption = UpdateContext.UpdateOptionProvider.ParseUpdateOption(xmlFile);

                UpdateContext.UpdateOption.InstalledVersion = Assembly.GetEntryAssembly().GetName().Version;

                UpdateContext.UpdateOption.IsUpdateAvailable = UpdateContext.UpdateOption.UpdateVersion > UpdateContext.UpdateOption.InstalledVersion;
            }
            catch (WebException)
            {
                CheckForUpdateEvent?.Invoke(new AutoUpdateArgs
                {
                    Message = ConstResources.UpdateXmlFileNotFound,
                    UpdateContext = UpdateContext
                });
                return false;
            }
            catch (Exception)
            {
                CheckForUpdateEvent?.Invoke(new AutoUpdateArgs
                {
                    Message = ConstResources.UpdateXmlFileEmpty,
                    UpdateContext = UpdateContext
                });
                return false;
            }
            return true;
        }

        private void StartUpdate()
        {
            if (!UpdateContext.UpdateOption.IsUpdateAvailable) return;

            if (UpdateContext.UpdateOption.UpdateMode != UpdateMode.Force)
            {
                var confirm = new ConfirmDiaglog(UpdateContext);
                if (confirm.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;
            }

            var download = new DownloadDiaglog(UpdateContext);
            if (download.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;

            ExecUpdateApp();
        }

        private void ExecUpdateApp()
        {
            if (UpdateContext.UpdateStartInfoProvider == null)
                return;

            var processInfo = UpdateContext.UpdateStartInfoProvider.ParseStartInfo(UpdateContext.AppUpdateArgs);
            Process.Start(processInfo);
        }
    }
}