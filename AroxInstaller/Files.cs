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
            tempDir.Create();
            tempDir.Attributes = FileAttributes.Hidden | FileAttributes.Directory;
        }

        public static void purgeTemp()
        {
            Directory.Delete(Config.TEMP_DIR, true);
        }
    }
}
