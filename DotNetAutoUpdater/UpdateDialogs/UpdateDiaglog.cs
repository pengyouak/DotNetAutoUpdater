using System;
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
            lblProcess.Text = $"{_updateToolOption.AppName} {_updateToolOption.PID}";

            // 复制文件

            // 启动进程

            //Finished();
        }

        private void Finished()
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}