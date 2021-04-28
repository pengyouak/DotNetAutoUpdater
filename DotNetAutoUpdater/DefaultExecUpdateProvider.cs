using System.IO;
using System.Windows.Forms;

namespace DotNetAutoUpdater
{
    internal class DefaultExecUpdateProvider : IExecUpdateProvider
    {
        public void ExecUpdate(AppUpdateArgs appUpdateArgs)
        {
            var items = XmlSerializerHelper.XmlDeSerializeObject<UpdateOption>(
               File.ReadAllText(Path.Combine(appUpdateArgs.TempFolderPath, appUpdateArgs.TempUpdateOption)));

            if (items == null)
            {
                MessageBox.Show(
                    ConstResources.UpdateNullUpdateOptionMessage,
                    ConstResources.UpdateNullUpdateOptionTitle,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            var dialog = new UpdateDialogs.UpdateDiaglog(appUpdateArgs, items);
            dialog.TopMost = true;
            dialog.ShowDialog();
        }
    }
}