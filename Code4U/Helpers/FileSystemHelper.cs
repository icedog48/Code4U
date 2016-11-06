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

        public static void WriteFile(string filename, string fileContent)
        {
            var directory = Path.GetDirectoryName(filename);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (File.Exists(filename))
            {
                var theirsFileContent = File.ReadAllText(filename);

                if (string.IsNullOrEmpty(theirsFileContent))
                {
                    File.WriteAllText(filename, fileContent);
                }
                else if (!theirsFileContent.Equals(fileContent))
                {
                    WriteConflictFile(filename, theirsFileContent, fileContent);
                }
            }
            else
            {
                File.WriteAllText(filename, fileContent);
            }
        }

        private static void WriteConflictFile(string filename, string theirsFileContent, string mineFileContent)
        {
            var theirsLines = theirsFileContent.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            var mineLines = mineFileContent.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            var fileContent = new List<string>();

            var lines = theirsLines.Length;

            if (mineLines.Length > lines) lines = mineLines.Length;

            var beginMark = "<<<<<<< Theirs";
            var middleMark = "=======";
            var endMark = ">>>>>>> Mine";

            for (int line = 0; line < lines; line++)
            {
                var mineLine = string.Empty;
                if (mineLines.Length > line) mineLine = mineLines[line];

                var theirsLine = string.Empty;
                if (theirsLines.Length > line) theirsLine = theirsLines[line];

                if (!theirsLine.Equals(mineLine))
                {
                    fileContent.Add(beginMark);
                    fileContent.Add(theirsLine);
                    fileContent.Add(middleMark);
                    fileContent.Add(mineLine);
                    fileContent.Add(endMark);
                }
                else
                {
                    fileContent.Add(mineLine);
                }
            }

            File.WriteAllLines(filename, fileContent.ToArray());
        }
    }
}
