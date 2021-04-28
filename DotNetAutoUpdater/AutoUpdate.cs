using DotNetAutoUpdater.UpdateDialogs;
using System;
using System.Net;
using System.Net.Cache;
using System.Reflection;
using System.Windows.Forms;

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

        public void Update(string url, int pid, string processName, string fileName)
        {
            UpdateContext = new UpdateContext(pid, processName, fileName);

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

                UpdateContext.UpdateOption.InstalledVersion = GetAppVersion();
                //UpdateContext.UpdateOption.InstalledVersion = Assembly.GetEntryAssembly().GetName().Version;

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

        private Version GetAppVersion()
        {
            try
            {
                if (!System.IO.File.Exists(UpdateContext.AppUpdateArgs.APPFullName)) return new Version(0, 0, 0, 0);

                var verInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(UpdateContext.AppUpdateArgs.APPFullName);
                return new Version(verInfo.FileVersion);
            }
            catch { return new Version(0, 0, 0, 0); }
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

            try
            {
                ExecUpdateApp();
            }
            catch (Exception ex)
            {
                MessageBox.Show("在更新时发生了异常，无法完成更新。" + ex.Message, "更新", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ExecUpdateApp()
        {
            if (UpdateContext.ExecUpdateProvider == null)
                return;

            UpdateContext.ExecUpdateProvider.ExecUpdate(UpdateContext.AppUpdateArgs);
        }
    }
}