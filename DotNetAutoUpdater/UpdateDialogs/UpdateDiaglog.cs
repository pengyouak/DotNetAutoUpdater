using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DotNetAutoUpdater.UpdateDialogs
{
    public partial class UpdateDiaglog : Form
    {
        private UpdateToolOption _updateToolOption;

        internal UpdateDiaglog(UpdateToolOption updateToolOption)
        {
            InitializeComponent();
            _updateToolOption = updateToolOption;
        }

        private void DownloadDiaglog_Load(object sender, EventArgs e)
        {
            this.Text = ConstResources.FormTextUpdateTitle;

            btnCancel.Text = ConstResources.ButtonTextUpdateCancel;

            lblTitle.Text = ConstResources.LabelTextUpdateTitle;
            lblSubTitle.Text = ConstResources.LabelTextUpdateSubTitle;
            lblProcess.Text = ConstResources.LabelTextUpdateTotalProcess;

            this.Size = this.MinimumSize;

            new TaskFactory().StartNew((new Action(() => BeginUpdate())));
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void BeginUpdate()
        {
            // 结束进程
            lblProcess.UpdateUI(() => lblProcess.Text = $"{_updateToolOption.AppName} {_updateToolOption.PID}");
            try
            {
                var fileInfo = new FileInfo(_updateToolOption.AppFullPath);
                BackupUpdate(fileInfo.DirectoryName);

                var processList = Process.GetProcesses();
                foreach (var item in processList)
                {
                    if (item.ProcessName == _updateToolOption.AppName) item.Kill();

                    if (item.Id == _updateToolOption.PID) item.Kill();
                }

                Thread.Sleep(500);

                InstallUpdate(fileInfo.DirectoryName);

                Clear();

                // 启动进程
                Process.Start(_updateToolOption.AppFullPath);

                Finished();
            }
            catch
            {
            }
        }

        private void BackupUpdate(string appFolder)
        {
            var backFolder = ConstResources.BackupFolder;
            if (!Directory.Exists(backFolder)) Directory.CreateDirectory(backFolder);

            try
            {
                foreach (var item in _updateToolOption.UpdateOption.UpdateItems)
                {
                    var backupPath = Path.Combine(backFolder, item.Path);
                    var filePath = Path.Combine(appFolder, item.Path);

                    if (File.Exists(filePath)) File.Copy(filePath, backupPath, true);
                }
            }
            catch { }
        }

        private void InstallUpdate(string appFolder)
        {
            if (!Directory.Exists(appFolder)) return;

            try
            {
                foreach (var item in _updateToolOption.UpdateOption.UpdateItems)
                {
                    var appPath = Path.Combine(appFolder, item.Path);
                    var updatePath = Path.Combine(ConstResources.UpdateFolder, item.Path);
                    var fileInfo = new FileInfo(appPath);

                    if (!Directory.Exists(fileInfo.DirectoryName)) Directory.CreateDirectory(fileInfo.DirectoryName);

                    File.Copy(updatePath, appPath, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Clear()
        {
            if (Directory.Exists(ConstResources.BackupFolder)) Directory.Delete(ConstResources.BackupFolder, true);
            if (Directory.Exists(ConstResources.UpdateFolder)) Directory.Delete(ConstResources.UpdateFolder, true);
            if (File.Exists(Path.Combine(ConstResources.TempFolder, ConstResources.TempUpdateOption)))
                File.Delete(Path.Combine(ConstResources.TempFolder, ConstResources.TempUpdateOption));

            var psi = new ProcessStartInfo("cmd.exe", $"/C ping 1.1.1.1 -n 1 -w 1000 > Nul & Del {Path.Combine(ConstResources.TempFolder, ConstResources.UpdateToolName)}");
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.CreateNoWindow = true;
            Process.Start(psi);
        }

        private void Finished()
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}