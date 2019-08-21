using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GitTableauCleanFilter
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                return;
            }

            var file = args[0];

            var pathConvertions = GetFilePathConvertions(file);

            var tableauxml = ReadTwbXmlStringFromInput();

            var outputxml = TalbaeuWorkbookCleanFilter.CleanTwbXml(tableauxml, pathConvertions);

            WriteTwbXmlStringToOutput(outputxml);
        }

        private static List<Tuple<string, string>> GetFilePathConvertions(string file)
        {
            var gitroot = (Environment.CurrentDirectory + Path.DirectorySeparatorChar).Replace(Path.DirectorySeparatorChar, '/');
            var filepath = Path.Combine(gitroot, file).Replace(Path.DirectorySeparatorChar, '/');
            var directryPath = Path.GetDirectoryName(filepath).Replace(Path.DirectorySeparatorChar, '/');
            var relativePathToGitRoot = new Uri(filepath).MakeRelativeUri(new Uri(gitroot)).ToString();

            var path_converts = new List<Tuple<string, string>>
            {
                new Tuple<string, string>(directryPath, "."),
                new Tuple<string, string>(gitroot, relativePathToGitRoot)
            };
            return path_converts;
        }

        private static void WriteTwbXmlStringToOutput(string outputxml)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine(outputxml);
        }

        private static string ReadTwbXmlStringFromInput()
        {
            Console.InputEncoding = Encoding.UTF8;
            var input = new StringBuilder();
            string line;
            while ((line = Console.ReadLine()) != null)
            {
                input.AppendLine(line);
            }

            return input.ToString();
        }
    }
}