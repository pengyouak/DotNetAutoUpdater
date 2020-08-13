using DotNetAutoUpdater;
using System;

namespace DotNetAutoUpdaterTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var op = new UpdateOption
            {
                UpdateMode = UpdateMode.PromptAndDetail,
                ChangeLog = "xxxxxxxx",
                Version = "1.1.1.1",
                UpdateItems = new System.Collections.Generic.List<UpdateItem>() {
                        new UpdateItem{
                             MinVersion="0.0.0.0",
                             ValideMode= ValideMode.Version,
                             Path="b",
                             Required=false
                        },
                         new UpdateItem{
                             MinVersion="0.0.0.0",
                             ValideMode= ValideMode.Version,
                             Path="c",
                             Required=false
                        },
                          new UpdateItem{
                             MinVersion="0.0.0.0",
                             ValideMode= ValideMode.Version,
                             Path="d",
                             Required=false
                        }
                    }
            };

            new AutoUpdate().Update(op);

            //var json = Newtonsoft.Json.JsonConvert.SerializeObject(op);
            //System.Console.WriteLine(json);
            //var opt = Newtonsoft.Json.JsonConvert.DeserializeObject<UpdateOption>(json);
            Console.ReadKey();
        }
    }
}