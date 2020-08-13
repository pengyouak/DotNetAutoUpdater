using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DotNetAutoUpdater.UpdateDialogs
{
    public partial class DownloadDiaglog : Form
    {
        private UpdateOption _updateOption;

        public DownloadDiaglog(UpdateOption updateOption)
        {
            InitializeComponent();
            _updateOption = updateOption;
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
            lblCurDownload.Text = ConstResources.LabelTextDownloadCurProgress;
            lblTotalDownload.Text = ConstResources.LabelTextDownloadTotalProgress;

            lsvUpdateItems.Columns.AddRange(new ColumnHeader[] {
                new ColumnHeader(){Name = "fileName", Width = 124, Text = ConstResources.ViewColTextDownloadFileName},
                new ColumnHeader(){Name = "ver", Width = 100, Text = ConstResources.ViewColTextDownloadUpdateVer},
                new ColumnHeader(){Name = "msg", Width = 290, Text = ConstResources.ViewColTextDownloadChangeLog},
            });
            this.Size = this.MinimumSize;

            LoadUpdateItem();

            new TaskFactory().StartNew((new Action(() => BeginDownload())));
        }

        private void LoadUpdateItem()
        {
            foreach (var item in _updateOption.UpdateItems)
            {
                lsvUpdateItems.Items.Add(new ListViewItem(new[] { item.Path, item.Version, item.ChangeLog }) { Tag = item });
            }
        }

        private void BeginDownload()
        {
            var tempFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConstResources.TempFolder);
            if (!System.IO.Directory.Exists(tempFolder)) System.IO.Directory.CreateDirectory(tempFolder);

            var evtPerDonwload = new ManualResetEvent(false);
            WebClient downloadClient = null;
            foreach (var item in _updateOption.UpdateItems)
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
                            lblCurDownload.Text = $"{item.Path}  {FormatBytesReceived(e.BytesReceived)}/{FormatBytesReceived(e.TotalBytesToReceive)}";
                        });
                        progressBarCurrent.UpdateUI(() =>
                        {
                            progressBarCurrent.Value = (int)((e.BytesReceived * 100) / e.TotalBytesToReceive);
                        });
                    };
                    downloadClient.DownloadFileCompleted += (sender, e) =>
                    {
                        lblCurDownload.UpdateUI(() =>
                        {
                            lblCurDownload.Text = ConstResources.LabelTextDownloadCurProgressFinished;
                        });

                        evtPerDonwload.Set();
                    };

                    evtPerDonwload.Reset();

                    downloadClient.DownloadFileAsync(new Uri("http://101.201.142.93:9000/update/auto-update.zip"),
                        System.IO.Path.Combine(tempFolder, "auto-update.zip"));

                    evtPerDonwload.WaitOne();

                    downloadClient.Dispose();
                    downloadClient = null;

                    break;
                }
                catch
                {
                    return;
                }
            }

            // handle files
        }

        private string FormatBytesReceived(long bytesReceived)
        {
            int index = 0;
            var unit = new[]{
                "KB","MB","GB","TB","PB","EB","ZB","YB"
            };

            Func<double, double> func = null;

            func = new Func<double, double>(value =>
            {
                if (index >= unit.Length) return value;

                var t = Math.Round(value / 1024.0, 2);
                if (t > 1024)
                {
                    index++;
                    return func(t);
                }
                return t;
            });

            return $"{func(bytesReceived)}{unit[index]}";
        }
    }
}