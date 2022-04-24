using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;

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

                var extension = Path.GetExtension(fileModel.Path);

                switch (extension)
                {
                    case ".exe":
                        await installExe(fileModel);
                        break;
                    case ".msi":
                        await installMsi(fileModel);
                        break;
                }
            }
        }

        private static Task installExe(FileModel file)
        {
            Utils.print("Installing " + file.Name);

            var process = Process.Start(file.Path, file.StartParams);
            process.WaitForExit();

            Utils.print("Installed " + file.Name);
            return Task.CompletedTask;
        }

        private static Task installMsi(FileModel file)
        {
            var fileInfo = new FileInfo(file.Path);
            Utils.print("Installing " + file.Name);

            var process = Process.Start("msiexec.exe", "/qn /norestart /i " + fileInfo.FullName);
            process.WaitForExit();

            Utils.print("Installed " + file.Name);
            return Task.CompletedTask;
        }
    }
}
