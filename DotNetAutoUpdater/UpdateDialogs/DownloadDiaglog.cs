using System;
using System.Windows.Forms;

namespace DotNetAutoUpdater.UpdateDialogs
{
    public partial class DownloadDiaglog : Form
    {
        public DownloadDiaglog()
        {
            InitializeComponent();
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
    }
}