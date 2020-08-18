using DotNetAutoUpdater;
using System;

namespace DotNetAutoUpdaterTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.ReadKey();

            new AutoUpdate().Update("http://101.201.142.93:18080/DotNetAutoUpdaterTest/update.xml");

            Console.ReadKey();
        }
    }
}