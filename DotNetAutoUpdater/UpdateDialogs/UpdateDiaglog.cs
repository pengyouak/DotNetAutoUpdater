using System;
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

        private void DownloadDiaglog_Load(object sender, EventArgs e)
        {
            this.Text = ConstResources.FormTextUpdateTitle;

            btnCancel.Text = ConstResources.ButtonTextUpdateCancel;

            lblTitle.Text = ConstResources.LabelTextUpdateTitle;
            lblSubTitle.Text = ConstResources.LabelTextUpdateSubTitle;
            lblTotalDownload.Text = ConstResources.LabelTextUpdateTotalProgress;

            this.Size = this.MinimumSize;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}