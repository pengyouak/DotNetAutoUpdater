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
                BackupUpdate(fileInfo.DirectoryName);

                var processList = Process.GetProcesses();
                foreach (var item in processList)
                {
                    if (item.ProcessName == _appUpdateInfoArgs.AppName) item.Kill();

                    if (item.Id == _appUpdateInfoArgs.PID) item.Kill();
                }

                Thread.Sleep(500);

                InstallUpdate(fileInfo.DirectoryName);

                Clear();

                // 启动进程
                Process.Start(_appUpdateInfoArgs.APPFullName);

                Finished();
            }
            catch
            {
            }
        }

        private void BackupUpdate(string appFolder)
        {
            var backFolder = _appUpdateInfoArgs.GetBackupFolderFullPath();
            if (!Directory.Exists(backFolder)) Directory.CreateDirectory(backFolder);

            try
            {
                foreach (var item in _updateOption.UpdateItems)
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
                foreach (var item in _updateOption.UpdateItems)
                {
                    var appPath = Path.Combine(appFolder, item.Path);
                    var updatePath = Path.Combine(_appUpdateInfoArgs.GetDownloadFolderFullPath(), item.Path);
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