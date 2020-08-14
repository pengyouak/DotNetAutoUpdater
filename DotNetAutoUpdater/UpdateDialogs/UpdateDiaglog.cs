﻿using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DotNetAutoUpdater.UpdateDialogs
{
    public partial class UpdateDiaglog : Form
    {
        private UpdateOption _updateOption;

        public UpdateDiaglog(UpdateOption updateOption)
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

            new TaskFactory().StartNew((new Action(() => BeginUpdate())));
        }

        private void LoadUpdateItem()
        {
            foreach (var item in _updateOption.UpdateItems)
            {
                lsvUpdateItems.Items.Add(new ListViewItem(new[] { item.Path, item.Version, item.ChangeLog }) { Tag = item });
            }
        }

        private void BeginUpdate()
        {
            var tempFolder = ConstResources.TempFolder;
            // template folder
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
            Directory.CreateDirectory(tempFolder);

            var evtPerDonwload = new ManualResetEvent(false);
            WebClient downloadClient = null;
            int index = 0;
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
                        // refresh total progress
                        progressBarTotal.UpdateUI(() =>
                        {
                            var progress = (int)(100 * ((index + (double)e.BytesReceived / e.TotalBytesToReceive) / _updateOption.UpdateItems.Count));
                            progressBarTotal.Value = progress > progressBarTotal.Maximum ? progressBarTotal.Maximum : progress;
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

                    var folder = Path.GetDirectoryName(Path.Combine(tempFolder, item.Path));
                    if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

                    downloadClient.DownloadFileAsync(new Uri(_updateOption.ServerUrl + item.Path),
                       Path.Combine(tempFolder, item.Path));
                    //downloadClient.DownloadFileAsync(new Uri("http://101.201.142.93:18080/DotNetAutoUpdaterTest/"),
                    //    Path.Combine(tempFolder, "auto-update.xml"));

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

            // backup
            Backup();
            Console.WriteLine(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName));
            // update

            // rollback
        }

        private void Backup()
        {
            var backFolder = ConstResources.BackupFolder;
            if (!Directory.Exists(backFolder)) Directory.CreateDirectory(backFolder);

            try
            {
                foreach (var item in _updateOption.UpdateItems)
                {
                    var backupPath = Path.Combine(backFolder, item.Path);
                    var filePath = Path.Combine(ConstResources.AppFolder, item.Path);

                    if (File.Exists(filePath)) File.Copy(filePath, backupPath, true);
                }
            }
            catch { }
        }

        private void Update()
        {
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
            Close();
        }
    }
}