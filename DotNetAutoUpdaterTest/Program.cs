using DotNetAutoUpdater;
using System;

namespace DotNetAutoUpdaterTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.ReadKey();

            new AutoUpdate().Update("http://xxx.xxx.xxx/update.xml");

            Console.ReadKey();
        }
    }
}