using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code4U.Helpers
{
    public static class FileSystemHelper
    {
        public static string[] GetDirectories(string baseDir)
        {
            var directories = new List<string>();

            directories.Add(baseDir);

            foreach (var directory in Directory.GetDirectories(baseDir))
            {
                directories.AddRange(GetDirectories(directory));
            }

            return directories.ToArray();
        }
    }
}
