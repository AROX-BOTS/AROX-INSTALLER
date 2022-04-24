using System.Drawing;

namespace AroxInstaller
{
    internal class Menu
    {
        private string selection;
        public async Task handle()
        {
            Console.Clear();
            Utils.print("Which version do you wish to install?", Color.Orange);
            Utils.print("[1] CLI", Color.Yellow);
            Utils.print("[2] UI", Color.Yellow);

            await getSelection();
        }

        private async Task getSelection()
        {
            var response = Console.ReadLine();

            while (response != "1" && response != "2")
            {
                await handle();
            }

            Console.Clear();
            selection = response;
            await handleSelection();
        }

        private async Task handleSelection()
        {
            switch (selection)
            {
                case "1": //CLI
                    await Installer.installCLI();
                    break;
                case "2": //UI
                    await Installer.installUI();
                    break;
            }
        }
    }
}
