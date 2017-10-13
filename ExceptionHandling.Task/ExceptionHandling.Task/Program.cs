using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExceptionHandling.Task.BL;

namespace ExceptionHandling.Task
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser parser = new Parser();
            while (true)
            {
                Console.WriteLine("Chose option:");
                Console.WriteLine("Get first symbol from string (press 1)");
                Console.WriteLine("Convert string to int (press 2)");
                string choose = Console.ReadLine();
                switch (choose)
                {
                    case "1":
                        Console.Write("Enter string:");
                        GetFirstSymbolFromInputString(parser, Console.ReadLine());
                        break;
                    case "2":
                        Console.Write("Enter number:");
                        ConvertStringToIntFromInputString(parser, Console.ReadLine());
                        break;

                }
            }
        }

        private static void GetFirstSymbolFromInputString(Parser parser, string str)
        {
            try
            {
                Console.WriteLine("You have entered:{0}", parser.GetFirstSymbol(str));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void ConvertStringToIntFromInputString(Parser parser, string str)
        {
            try
            {
                Console.WriteLine("You have entered:{0}", parser.ConvertStringToInt(str));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
