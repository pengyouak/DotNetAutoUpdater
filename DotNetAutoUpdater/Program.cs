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

            System.IO.File.WriteAllText("args.txt", string.Join(" ", args));

            if (args.Length == 0) return;

            var option = new UpdateToolOption();
            int index = 0;
            while (index < args.Length)
            {
                var name = args[index++];

                switch (name)
                {
                    case "/pid":
                        option.PID = int.Parse(args[index++]);
                        break;

                    case "/app":
                        option.AppFullPath = args[index++];
                        break;
                }
            }

            option.UpdateOption = XmlSerializerHelper.XmlDeSerializeObject<UpdateOption>(System.IO.File.ReadAllText(Path.Combine(AutoUpdate.UpdateContext.TempFolderPath, AutoUpdate.UpdateContext.TempUpdateOption)));

            if (option.UpdateOption == null) return;

            Application.Run(new UpdateDialogs.UpdateDiaglog(option));
        }
    }
}