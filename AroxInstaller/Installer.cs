using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;
using System.Drawing;

namespace AroxInstaller
{
    internal static class Installer
    {
        public static async Task installCLI()
        {
            await installNecessary();
            await downloadApi(Config.cliFiles);
        }

        public static async Task installUI()
        {
            await installNecessary();
            await downloadApi(Config.uiFiles);
        }

        private static async Task downloadApi(List<FileModel> fileModels)
        {
            foreach (var fileModel in fileModels)
            {
                using (var httpClient = new HttpClientWrapper(fileModel.Url))
                {
                    httpClient.setHeader(Config.API_HEADER_NAME, Config.API_HEADER_VALUE);
                    httpClient.setHeader("User-Agent", Config.API_USER_AGENT);

                    Utils.print("Downloading " + fileModel.Name, Color.Orange);
                    await httpClient.downloadFileAsync(fileModel.Path);
                    Utils.print("Downloaded " + fileModel.Name, Color.LimeGreen);
                    
                    var fileInfo = new FileInfo(fileModel.Path);

                    switch (fileInfo.Extension)
                    {
                        case ".exe":
                            Utils.print($"Moving {fileInfo.Name} to the root directory", Color.Green);
                            fileInfo.MoveTo(fileInfo.Name); //Move to root
                            break;
                        case ".zip":
                            Utils.print("Processing " + fileInfo.Name, Color.Green);
                            await processZipFile(fileInfo);
                            break;
                    }

                    if (fileModel.Delete)
                    {
                        Utils.print($"Deleting old {fileModel.Name} files", Color.GreenYellow);
                        File.Delete(fileModel.Path);
                    }
                }
            }
        }

        private static async Task processZipFile(FileInfo fileInfo)
        {

        }

        private static async Task installNecessary()
        {
            foreach (var fileModel in Config.files)
            {
                using (var httpClient = new HttpClientWrapper(fileModel.Url))
                {
                    Utils.print("Downloading " + fileModel.Name, Color.Orange);
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
            Utils.print("Installing " + file.Name, Color.Yellow);

            var process = Process.Start(file.Path, file.StartParams);
            process.WaitForExit();

            Utils.print("Installed " + file.Name, Color.LimeGreen);

            if (file.Delete)
            {
                Utils.print($"Deleting {file.Name} installer", Color.GreenYellow);
                File.Delete(file.Path);
            }

            return Task.CompletedTask;
        }

        private static Task installMsi(FileModel file)
        {
            var fileInfo = new FileInfo(file.Path);
            Utils.print("Installing " + file.Name, Color.Yellow);

            var process = Process.Start("msiexec.exe", "/qn /norestart /i " + fileInfo.FullName);
            process.WaitForExit();

            Utils.print("Installed " + file.Name, Color.LimeGreen);

            if (file.Delete)
            {
                Utils.print($"Deleting {file.Name} installer", Color.GreenYellow);
                File.Delete(file.Path);
            }

            return Task.CompletedTask;
        }
    }
}
