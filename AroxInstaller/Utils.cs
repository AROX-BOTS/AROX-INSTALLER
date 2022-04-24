using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AroxInstaller
{
    internal static class Utils
    {
        public static void setupConsole()
        {
            Console.Title = "Arox installer";
            print("Welcome to Arox installer!");
            print("Press any key to continue...");
            Console.ReadLine();
        }

        public static void print(string text)
        {
            Console.WriteLine($"[AROX] {text}");
        }
    }
}
