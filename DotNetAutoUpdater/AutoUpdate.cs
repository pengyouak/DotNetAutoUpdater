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
        #region public events

        public delegate void CheckForUpdateEventHandler(AutoUpdateArgs args);

        public event CheckForUpdateEventHandler CheckForUpdateEvent;

        #endregion public events

        #region public properties

        public static UpdateContext UpdateContext { get; set; }

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
                        Uri = uri,
                        Message = ConstResources.UpdateXmlFileEmpty,
                        UpdateContext = UpdateContext
                    });
                    return false;
                }
                UpdateContext.UpdateOption = UpdateContext.UpdateOptionHandler.ParseUpdateOption(xmlFile);

                UpdateContext.UpdateOption.InstalledVersion = Assembly.GetEntryAssembly().GetName().Version;

                UpdateContext.UpdateOption.IsUpdateAvailable = UpdateContext.UpdateOption.UpdateVersion > UpdateContext.UpdateOption.InstalledVersion;
            }
            catch (WebException)
            {
                CheckForUpdateEvent?.Invoke(new AutoUpdateArgs
                {
                    Uri = uri,
                    Message = ConstResources.UpdateXmlFileNotFound,
                    UpdateContext = UpdateContext
                });
                return false;
            }
            catch (Exception)
            {
                CheckForUpdateEvent?.Invoke(new AutoUpdateArgs
                {
                    Uri = uri,
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
                var confirm = new ConfirmDiaglog(UpdateContext.UpdateOption);
                if (confirm.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;
            }

            var download = new DownloadDiaglog(UpdateContext.UpdateOption);
            if (download.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;

            ExecUpdateApp();
        }

        private void ExecUpdateApp()
        {
            var updaterExe = Path.Combine(AutoUpdate.UpdateContext.TempFolderPath, AutoUpdate.UpdateContext.UpdateToolName);
            File.WriteAllBytes(updaterExe, Resources.DotNetAutoUpdater);

            var arguments = new StringBuilder();
            arguments.Append($"/pid {Process.GetCurrentProcess().Id} ");
            arguments.Append($"/app \"{Process.GetCurrentProcess().MainModule.FileName}\" ");

            var processInfo = new ProcessStartInfo(updaterExe, arguments.ToString())
            {
                UseShellExecute = true
            };
            Process.Start(processInfo);
        }
    }
}