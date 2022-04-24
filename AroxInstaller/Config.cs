using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AroxInstaller
{
    internal static class Config
    {
        public const string API_ENDPOINT = "https://aroxbots.com/api/win-installer/";
        public const string API_HEADER_NAME = "X-Bot-Key";
        public const string API_HEADER_VALUE = "db4718855207a2018bf144c6482fc8b267d37aa734f1e5be87294d3a7112012c";
        public const string API_USER_AGENT = "621c197ca68a33f800ca542539e90cc3e311ae3b6f917ef2d585ccdc6c04a666";
        public const string TEMP_DIR = "temp/";

        public static readonly List<FileModel> files = new()
        {
            new FileModel()
            {
                Name = "NodeJS",
                Path = TEMP_DIR + "nodejs.msi",
                Url = "https://nodejs.org/dist/v16.14.0/node-v16.14.0-x64.msi"
            },

            new FileModel()
            {
                Name = "Python",
                Path = TEMP_DIR + "python39.exe",
                Url = "https://www.python.org/ftp/python/3.9.0/python-3.9.0-amd64.exe"
            },

            new FileModel()
            {
                Name = "VC redistriduables",
                Path = TEMP_DIR + "vcredists.exe",
                Url = "http://aka.ms/vs/16/release/vc_redist.x64.exe"
            },

            new FileModel()
            {
                Name = ".NET Desktop runtime",
                Path = TEMP_DIR + "netruntime.exe",
                Url = "https://download.visualstudio.microsoft.com/download/pr/f13d7b5c-608f-432b-b7ec-8fe84f4030a1/5e06998f9ce23c620b9d6bac2dae6c1d/windowsdesktop-runtime-6.0.4-win-x64.exe"
            }
        };
    }
}
