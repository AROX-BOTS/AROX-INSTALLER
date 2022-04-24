using System.Threading.Tasks;
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