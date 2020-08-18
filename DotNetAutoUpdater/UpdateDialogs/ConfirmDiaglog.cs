using System.Reflection;
using System.Windows.Forms;

namespace DotNetAutoUpdater.UpdateDialogs
{
    public partial class ConfirmDiaglog : Form
    {
        public ConfirmDiaglog(UpdateOption updateOption)
        {
            InitializeComponent();
        }

        private void ConfirmDiaglog_Load(object sender, System.EventArgs e)
        {
            this.Text = ConstResources.FormTextConfirmTitle;

            lblTitle.Text = ConstResources.LabelTextConfirmTitle;
            lblSubTitle.Text = ConstResources.LabelTextConfirmSubTitle;

            btnUpdate.Text = ConstResources.ButtonTextConfirmUpdate;
            btnCancel.Text = ConstResources.ButtonTextConfirmCancel;
            btnRemindLater.Text = ConstResources.ButtonTextConfirmRemind;
            btnSkip.Text = ConstResources.ButtonTextConfirmSkip;

            lblFileName.Text = Assembly.GetEntryAssembly().GetName().Name;
            lblVersion.Text = $"{AutoUpdate.UpdateContext.UpdateOption.InstalledVersion} to {AutoUpdate.UpdateContext.UpdateOption.UpdateVersion}";
            txtChangeLog.Text = AutoUpdate.UpdateContext.UpdateOption.ChangeLog;

            if (AutoUpdate.UpdateContext.UpdateOption.UpdateMode == UpdateMode.PromptAndDetail)
            {
                txtChangeLog.Visible = true;
                this.Size = this.MaximumSize;
            }
            else if (AutoUpdate.UpdateContext.UpdateOption.UpdateMode == UpdateMode.Prompt)
            {
                txtChangeLog.Visible = false;
                this.Size = this.MinimumSize;
            }
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

        private void btnSkip_Click(object sender, System.EventArgs e)
        {
        }

        private void btnRemaindLater_Click(object sender, System.EventArgs e)
        {
        }
    }
}