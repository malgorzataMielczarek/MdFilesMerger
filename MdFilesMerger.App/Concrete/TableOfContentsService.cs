using MdFilesMerger.Domain.Common;
using MdFilesMerger.Domain.Entity;
using System.Text;

namespace MdFilesMerger.App.Concrete
{
    /// <summary>
    ///     Static class with method to create table of contents for specified merged file.
    /// </summary>
    public static class TableOfContentsService
    {
        /// <summary>
        ///     Creates the table of contents of specified type.
        /// </summary>
        /// <param name="mergedFile"> The merged file for which table of content will be created. </param>
        /// <param name="selectedFiles">
        ///     The list of all files, whose content will be placed in the merged file.
        /// </param>
        /// <returns> Text of whole table of contents. </returns>
        public static string? CreateTOC(MergedFile mergedFile, List<SelectedFile> selectedFiles) => mergedFile.TableOfContents switch
        {
            TableOfContents.Text => CreateTextTOC(selectedFiles, mergedFile.NewLineStyle),
            TableOfContents.Hyperlink => CreateHyperlinksTOC(selectedFiles, mergedFile.NewLineStyle),
            _ => null
        };

        private static int AppendDirectoriesEntries(List<string> appendedDirectories, StringBuilder stringBuilder, List<SelectedFile> listOfFiles, int currentIndex, string newLine)
        {
            SelectedFile currentFile = listOfFiles[currentIndex];
            return AppendDirectoriesEntries(appendedDirectories, stringBuilder, listOfFiles, currentFile, newLine);
        }

        private static int AppendDirectoriesEntries(List<string> appendedDirectories, StringBuilder stringBuilder, List<SelectedFile> listOfFiles, SelectedFile currentFile, string newLine)
        {
            string directories = "";
            string[] subDirectories = currentFile?.Name?.Split('/', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
            int dirNumber = subDirectories.Length - 1;

            if (dirNumber > 0)
            { // Don't add the name of the most nested directory if it is the only .md file in that directory
                string? directory = currentFile?.Name?.SkipLast(subDirectories[^1].Length + 1).ToString();
                if (!string.IsNullOrWhiteSpace(directory))
                {
                    foreach (var file in listOfFiles)
                    {
                        if (file != currentFile && file.Name!.Contains(directory))
                        {
                            dirNumber++;
                            break;
                        }
                    }
                    dirNumber--;
                }

                for (int i = 0; i < dirNumber; i++)
                {
                    string dir = subDirectories[i];
                    directories += "\\" + dir;

                    if (!appendedDirectories.Contains(directories))
                    {
                        appendedDirectories.Add(directories);

                        stringBuilder.Append('#', i + 3);
                        stringBuilder.Append(" ");
                        stringBuilder.Append(dir);
                        stringBuilder.Append(newLine);
                    }
                }
            }
            return dirNumber;
        }

        private static string CreateHyperlinksTOC(List<SelectedFile> listOfFiles, string newLine)
        {
            // Count files with the same title
            List<string> titlesOfFiles = new List<string>();
            Dictionary<string, int> titles = new Dictionary<string, int>();

            foreach (var file in listOfFiles)
            {
                if (file.Title == null)
                {
                    continue;
                }

                titlesOfFiles.Add(file.Title);

                if (titles.ContainsKey(file.Title))
                {
                    titles[file.Title]++;
                }
                else
                {
                    titles.Add(file.Title, 1);
                }
            }

            List<string> appendedDirectories = new List<string>();

            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < listOfFiles.Count; i++)
            {
                string title = titlesOfFiles[i];

                int dirNumber = AppendDirectoriesEntries(appendedDirectories, stringBuilder, listOfFiles, i, newLine);

                string hyperlink = Common.Hyperlinks.TextToHyperlink(title);

                int qtt = titles[title]++;
                hyperlink = hyperlink.Insert(hyperlink.Length - 1, "-" + qtt.ToString());

                stringBuilder.Append('#', dirNumber + 3);
                stringBuilder.Append(" ");
                stringBuilder.Append(hyperlink);
                stringBuilder.Append(newLine);
            }

            return stringBuilder.ToString();
        }

        private static string CreateTextTOC(List<SelectedFile> listOfFiles, string newLine)
        {
            List<string> appendedDirectories = new List<string>();

            StringBuilder stringBuilder = new StringBuilder();

            foreach (SelectedFile file in listOfFiles)
            {
                if (file.Title == null)
                {
                    continue;
                }

                int dirNumber = AppendDirectoriesEntries(appendedDirectories, stringBuilder, listOfFiles, file, newLine);

                stringBuilder.Append('#', dirNumber + 3);
                stringBuilder.Append(" ");
                stringBuilder.Append(file.Title);
                stringBuilder.Append(newLine);
            }

            return stringBuilder.ToString();
        }
    }
}