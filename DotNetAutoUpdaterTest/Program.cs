using DotNetAutoUpdater;
using System;

namespace DotNetAutoUpdaterTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //var op = new UpdateOption
            //{
            //    UpdateMode = UpdateMode.Force,
            //    ChangeLog = "xxxxxxxx\nxxxxxxxx\nxxxxxxxx\nxxxxxxxx\nxxxxxxxx\nxxxxxxxx\nxxxxxxxx\nxxxxxxxx\nxxxxxxxx\nxxxxxxxx\nxxxxxxxx\nxxxxxxxx\nxxxxxxxx\nxxxxxxxx\nxxxxxxxx\nxxxxxxxx\n",
            //    Version = "1.1.1.1",
            //    ServerUrl = "http://101.201.142.93:18080/DotNetAutoUpdaterTest/",
            //    UpdateItems = new System.Collections.Generic.List<UpdateItem>() {
            //            new UpdateItem{
            //                 MinVersion="0.0.0.0",
            //                 Version="1.0.0.0",
            //                 ValideMode= ValideMode.Version,
            //                 Path="DotNetAutoUpdater.dll",
            //                 Required=false
            //            },
            //            new UpdateItem{
            //                 MinVersion="0.0.0.0",
            //                 Version="1.0.0.0",
            //                 ValideMode= ValideMode.Version,
            //                 Path="DotNetAutoUpdaterTest.exe",
            //                 Required=false
            //            },
            //            new UpdateItem{
            //                 MinVersion="0.0.0.0",
            //                 Version="1.0.0.0",
            //                 ValideMode= ValideMode.Version,
            //                 Path="b/DotNetAutoUpdaterTest.exe",
            //                 Required=false
            //            },
            //            new UpdateItem{
            //                 MinVersion="0.0.0.0",
            //                 Version="1.0.0.0",
            //                 ValideMode= ValideMode.Version,
            //                 Path="a/DotNetAutoUpdater.dll",
            //                 Required=false
            //            }
            //        }
            //};

            Console.ReadKey();
            //ZipFile(@"D:\git\DotNetAutoUpdater\Tools\DotNetAutoUpdater.exe");

            //new AutoUpdate().Update(op);
            new AutoUpdate().Update("http://101.201.142.93:18080/DotNetAutoUpdaterTest/update.xml");

            Console.ReadKey();
        }

        private static void ZipFile(string file)
        {
            var target = file + ".gz";
            using (var ts = System.IO.File.OpenWrite(target))
            using (var gz = new System.IO.Compression.GZipStream(ts, System.IO.Compression.CompressionMode.Compress))
            {
                var buffer = System.IO.File.ReadAllBytes(file);
                gz.Write(buffer, 0, buffer.Length);
            }
        }
    }
}