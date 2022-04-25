using System.Drawing;

namespace AroxInstaller
{
    class Program
    {
        public static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        public static async Task MainAsync(string[] args)
        {
            Utils.setupConsole();

            if (!Utils.IsRanAsAdmin())
            {
                Utils.print("Run the installer as administrator!", Color.Red);
                Console.ReadLine();
                return;
            }

            if (await Utils.isOutdated())
            {
                Utils.print("You are running an outdated version, re-download the installer!", Color.Red);
                Console.ReadLine();
                return;
            }

            Files.prepare();

            var menu = new Menu();
            await menu.handle();
            Utils.print("DONE!!!!!", Color.Lime);
            Utils.print("Press any key to exit", Color.Yellow);
            Console.ReadLine();
        }
    }
}