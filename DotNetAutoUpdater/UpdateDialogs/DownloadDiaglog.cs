using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DotNetAutoUpdater.UpdateDialogs
{
    public partial class DownloadDiaglog : Form
    {
        private SynchronizationContext _synchronizationContext;
        private UpdateContext _updateContext;

        public DownloadDiaglog(UpdateContext updateContext)
        {
            InitializeComponent();
            _updateContext = updateContext;
            _synchronizationContext = SynchronizationContext.Current; ;
        }

        private void btnShowList_Click(object sender, EventArgs e)
        {
            if (this.Size == this.MinimumSize)
            {
                this.Size = this.MaximumSize;
                btnShowList.Text = ConstResources.ButtonTextHideDetail;
            }
            else
            {
                this.Size = this.MinimumSize;
                btnShowList.Text = ConstResources.ButtonTextShowDetail;
            }
        }

        private void DownloadDiaglog_Load(object sender, EventArgs e)
        {
            this.Text = ConstResources.FormTextDownloadTitle;

            btnShowList.Text = ConstResources.ButtonTextShowDetail;
            btnCancel.Text = ConstResources.ButtonTextDownloadCancel;

            lblTitle.Text = ConstResources.LabelTextDownloadTitle;
            lblSubTitle.Text = ConstResources.LabelTextDownloadSubTitle;
            lblCurDownload.Text = ConstResources.LabelTextDownloadCurProcess;
            lblTotalDownload.Text = ConstResources.LabelTextDownloadTotalProcess;

            lsvUpdateItems.Columns.AddRange(new ColumnHeader[] {
                new ColumnHeader(){Name = "fileName", Width = 124, Text = ConstResources.ViewColTextDownloadFileName},
                new ColumnHeader(){Name = "ver", Width = 100, Text = ConstResources.ViewColTextDownloadUpdateVer},
                new ColumnHeader(){Name = "msg", Width = 290, Text = ConstResources.ViewColTextDownloadChangeLog},
            });
            this.Size = this.MinimumSize;

            LoadUpdateItem();

            new TaskFactory(TaskCreationOptions.AttachedToParent, TaskContinuationOptions.AttachedToParent)
                .StartNew((new Action(() => BeginDownload())));
        }

        private void LoadUpdateItem()
        {
            foreach (var item in _updateContext.UpdateOption.UpdateItems)
            {
                lsvUpdateItems.Items.Add(new ListViewItem(new[] { item.Path, item.Version, item.ChangeLog }) { Tag = item });
            }
        }

        private void BeginDownload()
        {
            var tempFolder = _updateContext.AppUpdateInfoArgs.GetDownloadFolderFullPath();
            // template folder
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
            Directory.CreateDirectory(tempFolder);

            var startedAt = DateTime.Now;

            var evtPerDonwload = new ManualResetEvent(false);
            WebClient downloadClient = null;
            int index = 0;
            foreach (var item in _updateContext.UpdateOption.UpdateItems)
            {
                try
                {
                    downloadClient = new WebClient();
                    downloadClient.Proxy = WebRequest.DefaultWebProxy;
                    downloadClient.Proxy.Credentials = CredentialCache.DefaultCredentials;
                    downloadClient.Credentials = CredentialCache.DefaultCredentials;
                    downloadClient.DownloadProgressChanged += (sender, e) =>
                    {
                        lblCurDownload.UpdateUI(() =>
                        {
                            var timespan = DateTime.Now - startedAt;
                            var totalSeconds = (long)timespan.TotalSeconds;
                            if (totalSeconds > 0)
                                lblCurDownload.Text = $"{item.Path}  {FormatBytesReceived(e.BytesReceived)}/{FormatBytesReceived(e.TotalBytesToReceive)} {FormatBytesReceived(e.BytesReceived / totalSeconds)}/s";
                        });
                        progressBarCurrent.UpdateUI(() =>
                        {
                            progressBarCurrent.Value = e.ProgressPercentage;
                        });
                        // refresh total progress
                        progressBarTotal.UpdateUI(() =>
                        {
                            var progress = 100 * ((index + e.ProgressPercentage / 100) / _updateContext.UpdateOption.UpdateItems.Count);
                            progressBarTotal.Value = progress > progressBarTotal.Maximum ? progressBarTotal.Maximum : progress;
                        });
                    };
                    downloadClient.DownloadFileCompleted += (sender, e) =>
                    {
                        if (e.Cancelled) return;

                        if (e.Error != null) throw e.Error;

                        lblCurDownload.UpdateUI(() =>
                        {
                            lblCurDownload.Text = ConstResources.LabelTextDownloadCurProcessFinished;
                        });

                        evtPerDonwload.Set();
                    };

                    evtPerDonwload.Reset();

                    var folder = Path.GetDirectoryName(Path.Combine(tempFolder, item.Path));
                    if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

                    downloadClient.DownloadFileAsync(new Uri(_updateContext.UpdateOption.ServerUrl + item.Path),
                       Path.Combine(tempFolder, item.Path));

                    evtPerDonwload.WaitOne();

                    downloadClient.Dispose();
                    downloadClient = null;
                }
                catch
                {
                    return;
                }
                index++;
            }

            XmlSerializerHelper.XmlSerializeObject(_updateContext.UpdateOption,
                Path.Combine(_updateContext.AppUpdateInfoArgs.TempFolderPath, _updateContext.AppUpdateInfoArgs.TempUpdateOption));

            DialogResult = DialogResult.OK;

            _synchronizationContext.Send(new SendOrPostCallback(obj => { Close(); }), null);
        }

        private string FormatBytesReceived(long bytesReceived)
        {
            var unit = new[]{
                "B","KB","MB","GB","TB","PB","EB","ZB","YB"
            };

            if (bytesReceived == 0)
                return $"0{unit[0]}";

            int place = (int)Math.Floor(Math.Log(bytesReceived, 1024));
            double num = Math.Round(bytesReceived / Math.Pow(1024, place), 2);

            return $"{num}{unit[place]}";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}