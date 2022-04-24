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
            Files.prepare();

            var menu = new Menu();
            await menu.handle();
            Utils.print("DONE!!!!!", Color.Lime);
            Utils.print("Press any key to exit", Color.Yellow);
            Console.ReadLine();
        }
    }
}