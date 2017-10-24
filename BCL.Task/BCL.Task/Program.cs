using System;
using System.Configuration;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using BCL.Task.Configuration;

namespace BCL.Task
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CancelKeyPress += Console_CancelKeyPress;
            var config = (ProgConfigurationSection)
                ConfigurationManager.GetSection("customConfigurationSection");
            FileManager fm = new FileManager(config);
            fm.StartWatch();
            Console.ReadKey();
        }

        private static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
