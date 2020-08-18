using System;
using System.IO;
using System.Windows.Forms;

namespace DotNetAutoUpdater
{
    public static class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            File.WriteAllText("args.txt", string.Join(" ", args));

            if (args.Length == 0) return;

            int pid = 0;
            string appFullName = "";
            int index = 0;
            while (index < args.Length)
            {
                var name = args[index++];

                switch (name)
                {
                    case "-p":
                    case "/pid":
                        pid = int.Parse(args[index++]);
                        break;

                    case "-a":
                    case "/app":
                        appFullName = args[index++];
                        break;
                }
            }

            if (pid <= 0 && string.IsNullOrEmpty(appFullName))
            {
                throw new ArgumentNullException(string.Join(" ", args), ConstResources.UpdateInvalidArgsMessage);
            }

            var fileName = Path.GetFileName(appFullName);

            var option = new AppUpdateArgs(pid, fileName, appFullName);

            var items = XmlSerializerHelper.XmlDeSerializeObject<UpdateOption>(
                File.ReadAllText(Path.Combine(option.TempFolderPath, option.TempUpdateOption)));

            if (items == null)
            {
                MessageBox.Show(
                    ConstResources.UpdateNullUpdateOptionMessage,
                    ConstResources.UpdateNullUpdateOptionTitle,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            Application.Run(new UpdateDialogs.UpdateDiaglog(option, items));
        }
    }
}