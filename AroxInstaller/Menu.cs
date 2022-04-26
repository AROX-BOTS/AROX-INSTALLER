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
                    await Installer.installCLI(this);
                    break;
                case "2": //UI
                    await Installer.installUI(this);
                    break;
            }
        }

        public Task<bool> askDependencies()
        {
            var response = "";
            do { 
                Console.Clear();
                Utils.print("Do you wish to install necessary dependencies?", Color.Yellow);
                Utils.print("This is usually required only once!", Color.Red);
                Utils.print("[1] Yes");
                Utils.print("[2] No");
            } while (response != "1" && response != "2");
            
            return Task.FromResult(response == "1");
        }
    }
}
