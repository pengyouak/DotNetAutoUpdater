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
#if !DEBUG
            // 更新程序在临时目录启动
            var exe = new System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var path = AppDomain.CurrentDomain.BaseDirectory;
            var tempPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "DotNetAutoUpdater.exe");
            if (exe.Directory.FullName != tempPath.TrimEnd('\\'))
            {
                if (!System.IO.Directory.Exists(tempPath)) System.IO.Directory.CreateDirectory(tempPath);
                var target = System.IO.Path.Combine(tempPath, exe.Name);
                try
                {
                    System.IO.File.Copy(exe.FullName, target, true);
                }
                catch { }
                System.Diagnostics.Process.Start(target, string.Join(" ", args));
                return;
            }
#endif

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length == 0) return;

            int pid = 0;
            string url = "";
            string appFullName = "";
            int index = 0;
            while (index < args.Length)
            {
                var name = args[index++];

                switch (name)
                {
                    // 更新服务地址
                    case "-u":
                    case "-url":
                        url = args[index++];
                        break;

                    // 要更新的程序的进程id
                    case "-p":
                    case "/pid":
                        pid = int.Parse(args[index++]);
                        break;

                    // 要更新的程序的完整路径
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

            try
            {
                new AutoUpdate().Update(url, pid, fileName, appFullName);
            }
            catch { }

            // choice /C YN /T 5 /D Y /N >nul || ping www.baidu.com
        }
    }
}