using System.Windows.Forms;

namespace DotNetAutoUpdater
{
    public static class AutoUpdateExtensions
    {
        public static void UpdateUI(this Control control, MethodInvoker methodInvoker)
        {
            if (control.InvokeRequired)
            {
                while (!control.IsHandleCreated)
                {
                    if (control.Disposing || control.IsDisposed)
                        return;
                }
                control.Invoke(methodInvoker);
            }
            else
                methodInvoker();
        }
    }
}