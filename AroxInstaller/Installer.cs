using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace AroxInstaller
{
    internal static class Installer
    {
        public static async Task installCLI()
        {
            await installNecessary();
        }

        public static async Task installUI()
        {
            await installNecessary();
        }

        private static async Task installNecessary()
        {
            foreach (var fileModel in Config.files)
            {
                using (var httpClient = new HttpClientWrapper(fileModel.Url))
                {
                    Utils.print("Downloading " + fileModel.Name);
                    await httpClient.downloadFileAsync(fileModel.Path);
                }
            }
        }
    }
}
