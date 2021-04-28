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
        private AppUpdateArgs _appUpdateInfoArgs;
        private UpdateOption _updateOption;

        internal UpdateDiaglog(AppUpdateArgs appUpdateInfoArgs, UpdateOption updateOption)
        {
            InitializeComponent();
            _appUpdateInfoArgs = appUpdateInfoArgs;
            _updateOption = updateOption;
        }

        private void DownloadDiaglog_Load(object sender, EventArgs e)
        {
            this.Text = ConstResources.FormTextUpdateTitle;

            btnCancel.Text = ConstResources.ButtonTextUpdateCancel;

            lblTitle.Text = ConstResources.LabelTextUpdateTitle;
            lblSubTitle.Text = ConstResources.LabelTextUpdateSubTitle;
            lblProcess.Text = ConstResources.LabelTextUpdateTotalProcess;

            this.Size = this.MinimumSize;
            progressBarTotal.Maximum = _updateOption.UpdateItems.Count;
            progressBarTotal.Value = 0;

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
            lblProcess.UpdateUI(() => lblProcess.Text = $"{_appUpdateInfoArgs.AppName} {_appUpdateInfoArgs.PID}");
            try
            {
                var fileInfo = new FileInfo(_appUpdateInfoArgs.APPFullName);
                BackupUpdate();

                var processList = Process.GetProcesses();
                foreach (var item in processList)
                {
                    if (item.ProcessName == _appUpdateInfoArgs.AppName) item.Kill();

                    if (item.Id == _appUpdateInfoArgs.PID) item.Kill();
                }

                Thread.Sleep(500);

                if (!InstallUpdate())
                    RestoreUpdate();

                Clear();

                // 启动进程
                Process.Start(_appUpdateInfoArgs.APPFullName);

                lblProcess.UpdateUI(() => lblProcess.Text = ConstResources.LabelTextUpdateCompleted);

                Thread.Sleep(500);

                Finished();
            }
            catch
            {
            }
        }

        private void BackupUpdate()
        {
            var fileInfo = new FileInfo(_appUpdateInfoArgs.APPFullName);
            var backFolder = _appUpdateInfoArgs.GetBackupFolderFullPath();
            if (!Directory.Exists(backFolder)) Directory.CreateDirectory(backFolder);
            lblProcess.UpdateUI(() => lblProcess.Text = ConstResources.LabelTextUpdateBackup);

            try
            {
                foreach (var item in _updateOption.UpdateItems)
                {
                    var backupPath = Path.Combine(backFolder, item.Path);
                    var filePath = Path.Combine(fileInfo.DirectoryName, item.Path);

                    var fi = new System.IO.FileInfo(backupPath);
                    if (!fi.Directory.Exists) Directory.CreateDirectory(fi.Directory.FullName);

                    if (File.Exists(filePath)) File.Copy(filePath, backupPath, true);
                }
            }
            catch { }
        }

        private bool InstallUpdate()
        {
            var fileInfo = new FileInfo(_appUpdateInfoArgs.APPFullName);
            if (!Directory.Exists(fileInfo.DirectoryName)) return true;
            lblProcess.UpdateUI(() => lblProcess.Text = ConstResources.LabelTextUpdating);

            try
            {
                foreach (var item in _updateOption.UpdateItems)
                {
                    var appPath = Path.Combine(fileInfo.DirectoryName, item.Path);
                    var updatePath = Path.Combine(_appUpdateInfoArgs.GetDownloadFolderFullPath(), item.Path);
                    var appFileInfo = new FileInfo(appPath);

                    if (!Directory.Exists(appFileInfo.DirectoryName)) Directory.CreateDirectory(appFileInfo.DirectoryName);

                    File.Copy(updatePath, appPath, true);
                    progressBarTotal.UpdateUI(() => progressBarTotal.Value += 1);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void RestoreUpdate()
        {
            var fileInfo = new FileInfo(_appUpdateInfoArgs.APPFullName);
            var backFolder = _appUpdateInfoArgs.GetBackupFolderFullPath();
            lblProcess.UpdateUI(() => lblProcess.Text = ConstResources.LabelTextUpdateRestore);

            try
            {
                foreach (var item in _updateOption.UpdateItems)
                {
                    var backupPath = Path.Combine(backFolder, item.Path);
                    var filePath = Path.Combine(fileInfo.DirectoryName, item.Path);

                    File.Copy(backupPath, filePath, true);
                    progressBarTotal.UpdateUI(() => progressBarTotal.Value -= 1);
                }
            }
            catch { }
        }

        private void Clear()
        {
            if (Directory.Exists(_appUpdateInfoArgs.GetBackupFolderFullPath())) Directory.Delete(_appUpdateInfoArgs.GetBackupFolderFullPath(), true);
            if (Directory.Exists(_appUpdateInfoArgs.GetDownloadFolderFullPath())) Directory.Delete(_appUpdateInfoArgs.GetDownloadFolderFullPath(), true);
            if (File.Exists(Path.Combine(_appUpdateInfoArgs.TempFolderPath, _appUpdateInfoArgs.TempUpdateOption)))
                File.Delete(Path.Combine(_appUpdateInfoArgs.TempFolderPath, _appUpdateInfoArgs.TempUpdateOption));

            var psi = new ProcessStartInfo("cmd.exe", $"/C ping 1.1.1.1 -n 1 -w 1000 > Nul & Del {Path.Combine(_appUpdateInfoArgs.TempFolderPath, _appUpdateInfoArgs.UpdateToolName)}");
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