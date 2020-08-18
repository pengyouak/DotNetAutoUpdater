﻿using System;
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

            var option = new AppUpdateInfoArgs();
            int index = 0;
            while (index < args.Length)
            {
                var name = args[index++];

                switch (name)
                {
                    case "-p":
                    case "/pid":
                        option.PID = int.Parse(args[index++]);
                        break;

                    case "-a":
                    case "/app":
                        option.APPFullName = args[index++];
                        break;
                }
            }

            var items = XmlSerializerHelper.XmlDeSerializeObject<UpdateOption>(
                File.ReadAllText(Path.Combine(option.TempFolderPath, option.TempUpdateOption)));

            Application.Run(new UpdateDialogs.UpdateDiaglog(option, items));
        }
    }
}