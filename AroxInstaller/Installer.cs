using System.Diagnostics;
using System.Drawing;
using System.IO.Compression;

namespace AroxInstaller
{
    internal static class Installer
    {
        public static async Task installCLI(Menu menu)
        {
            if (await menu.askDependencies())
            {
                await installNecessary();
            }

            await downloadApi(Config.cliFiles);
            Files.purgeTemp();
        }

        public static async Task installUI(Menu menu)
        {
            if (await menu.askDependencies())
            {
                await installNecessary();
            }

            await downloadApi(Config.uiFiles);
            Files.purgeTemp();
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
                            fileInfo.MoveTo(fileInfo.Name, true); //Move to root
                            break;
                        case ".zip":
                            Utils.print("Processing " + fileInfo.Name, Color.Yellow);
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
            using (var zipFile = ZipFile.OpenRead(fileInfo.FullName))
            {
                foreach (var entry in zipFile.Entries)
                {
                    var extension = Path.GetExtension(entry.Name);

                    switch (extension)
                    {
                        case ".dll":
                            Utils.print($"Extracting {entry.Name} to root directory", Color.Green);
                            entry.ExtractToFile(entry.Name, true);
                            break;
                        case ".gz":
                            Utils.print($"Extracting {entry.Name} to temp directory", Color.Green);
                            var path = Config.TEMP_DIR + entry.Name;
                            entry.ExtractToFile(path, true);

                            Utils.print($"Installing {entry.Name} via pip3", Color.Yellow);
                            await installPipPackage(new FileInfo(path)); //need absolute path

                            Utils.print($"Installed {entry.Name} via pip3", Color.LimeGreen);
                            Utils.print($"Deleting {entry.Name} from temp directory", Color.YellowGreen);
                            File.Delete(path);
                            break;
                    }
                }
            }
        }

        private static Task installNpmPackage(string name)
        {
            var npmProcess = Process.Start("cmd.exe", "/C npm install -g " + name);
            npmProcess.WaitForExit();
            return Task.CompletedTask;
        }

        private static Task installPipPackage(string name)
        {
            var pipProcess = Process.Start("cmd.exe", "/C pip3 install -U " + name);
            pipProcess.WaitForExit();
            return Task.CompletedTask;
        }

        private static Task installPipPackage(FileInfo file)
        {
            var pipProcess = Process.Start("cmd.exe", "/C pip3 install -U " + file.FullName);
            pipProcess.WaitForExit();
            return Task.CompletedTask;
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

            Utils.print("Installing captcha-harvester via pip", Color.Yellow);
            await installPipPackage("captcha-harvester");
            Utils.print("Installed captcha-harvester", Color.LimeGreen);

            Utils.print("Installing near-cli via npm", Color.Yellow);
            await installNpmPackage("near-cli");
            Utils.print("Installed near-cli", Color.LimeGreen);
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
