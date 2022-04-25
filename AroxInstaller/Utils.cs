using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Principal;
using Console = Colorful.Console;

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

        public static async Task<bool> isOutdated()
        {
            using (var client = new HttpClientWrapper(Config.API_ENDPOINT + "version"))
            {
                var serverVersion = await client.getString();
                return serverVersion == Config.VERSION;
            }
        }
    }
}
