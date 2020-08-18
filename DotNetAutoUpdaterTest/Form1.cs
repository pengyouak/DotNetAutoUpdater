using DotNetAutoUpdater;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace DotNetAutoUpdaterTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblVer.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private void btnCheckUpdate_Click(object sender, EventArgs e)
        {
            new AutoUpdate().Update("http://101.201.142.93:18080/DotNetAutoUpdaterTest/update.xml");
        }
    }
}