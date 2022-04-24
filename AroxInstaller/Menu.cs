using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AroxInstaller
{
    internal class Menu
    {
        private string selection;
        public async Task handle()
        {
            Console.Clear();
            Utils.print("Which version do you wish to install?");
            Utils.print("[1] CLI");
            Utils.print("[2] UI");

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
