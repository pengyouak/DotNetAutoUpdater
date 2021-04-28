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
            var updaterPath = "NetEaseHelper.AutoUpdater.exe";
            if (System.IO.File.Exists(updaterPath))
            {
                string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, updaterPath);
                var update = System.Diagnostics.Process.Start(path, $@"-u http://101.201.142.93:18080/DotNetAutoUpdaterTest/update.xml -p {System.Diagnostics.Process.GetCurrentProcess().Id} -a ""{System.Windows.Forms.Application.ExecutablePath}""");
                update.WaitForExit();
            }
        }
    }
}