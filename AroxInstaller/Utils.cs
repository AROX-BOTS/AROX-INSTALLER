using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Colorful.Console;
using System.Drawing;
using System.Security.Principal;
using System.Runtime.InteropServices;

namespace AroxInstaller
{
    internal static class Utils
    {
        public static void setupConsole()
        {
            Console.Title = "Arox installer";
            print("Welcome to Arox installer!", Color.Orange);
            print("Press any key to continue...", Color.Yellow);
            Console.ReadLine();
        }

        public static void print(string text, Color color)
        {
            Console.WriteLine($"[AROX] {text}", color);
        }

        public static bool IsRanAsAdmin()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                using (WindowsIdentity Identity = WindowsIdentity.GetCurrent())
                {
                    WindowsPrincipal Principal = new WindowsPrincipal(Identity);

                    return Principal.IsInRole(WindowsBuiltInRole.Administrator);
                }
            }

            //not aware of any MACOS check for is admin
            return true;
        }
    }
}
