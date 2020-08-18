using DotNetAutoUpdater.Properties;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace DotNetAutoUpdater
{
    internal class DefaultUpdateStartInfoProvider : IUpdateStartInfoProvider
    {
        public ProcessStartInfo ParseStartInfo(AppUpdateArgs args)
        {
            var arguments = Environment.GetCommandLineArgs().ToList();
            arguments.Add("/pid");
            arguments.Add(args.PID.ToString());
            arguments.Add("/app");
            arguments.Add($"\"{args.APPFullName}\"");

            var updaterExe = Path.Combine(args.TempFolderPath, args.UpdateToolName);
            File.WriteAllBytes(updaterExe, Resources.DotNetAutoUpdater);

            return new ProcessStartInfo(updaterExe, string.Join(" ", arguments))
            {
                UseShellExecute = true
            };
        }
    }
}