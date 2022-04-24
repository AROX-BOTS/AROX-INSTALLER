using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AroxInstaller
{
    internal static class Files
    {
        public static void prepare()
        {
            if (Directory.Exists(Config.TEMP_DIR))
            {
                purgeTemp();
            }

            var tempDir = new DirectoryInfo(Config.TEMP_DIR);

            Utils.print("Preparing file structure");

            tempDir.Create();
            tempDir.Attributes = FileAttributes.Hidden | FileAttributes.Directory;
        }

        private static void purgeTemp()
        {
            Utils.print("Purging old files");
            Directory.Delete(Config.TEMP_DIR, true);
        }
    }
}
