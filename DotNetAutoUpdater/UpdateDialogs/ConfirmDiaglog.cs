using System.Reflection;
using System.Windows.Forms;

namespace DotNetAutoUpdater.UpdateDialogs
{
    public partial class ConfirmDiaglog : Form
    {
        private UpdateOption _updateOption;

        public ConfirmDiaglog(UpdateOption updateOption)
        {
            InitializeComponent();
            _updateOption = updateOption;
        }

        private void ConfirmDiaglog_Load(object sender, System.EventArgs e)
        {
            lblTitle.Text = ConstResources.LabelTextConfirmTitle;
            lblSubTitle.Text = ConstResources.LabelTextConfirmSubTitle;

            btnUpdate.Text = ConstResources.ButtonTextConfirmUpdate;
            btnCancel.Text = ConstResources.ButtonTextConfirmCancel;

            lblFileName.Text = Assembly.GetEntryAssembly().GetName().Name;
            lblVersion.Text = $"{_updateOption.InstalledVersion} to {_updateOption.UpdateVersion}";
            txtChangeLog.Text = _updateOption.ChangeLog;
        }

        private void btnUpdate_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}