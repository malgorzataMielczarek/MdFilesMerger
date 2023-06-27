using MdFilesMerger.Domain.Common;
using MdFilesMerger.Domain.Abstract;
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
        /// <param name="mergedFile">
        ///     The merged file for which table of content will be created.
        /// </param>
        /// <param name="selectedFiles">
        ///     The list of all files, whose content will be placed in the merged file.
        /// </param>
        /// <returns> Text of whole table of contents. </returns>
        public static string? CreateTOC(IMergedFile mergedFile, List<ISelectedFile> selectedFiles) => mergedFile.TableOfContents switch
        {
            TableOfContents.Text => CreateTextTOC(selectedFiles, mergedFile.NewLineStyle),
            TableOfContents.Hyperlink => CreateHyperlinksTOC(selectedFiles, mergedFile.NewLineStyle),
            _ => null
        };

        private static int AppendDirectoriesEntries(List<string> appendedDirectories, StringBuilder stringBuilder, List<ISelectedFile> listOfFiles, int currentIndex, string newLine)
        {
            ISelectedFile currentFile = listOfFiles[currentIndex];
            return AppendDirectoriesEntries(appendedDirectories, stringBuilder, listOfFiles, currentFile, newLine);
        }

        private static int AppendDirectoriesEntries(List<string> appendedDirectories, StringBuilder stringBuilder, List<ISelectedFile> listOfFiles, ISelectedFile currentFile, string newLine)
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

        private static string CreateHyperlinksTOC(List<ISelectedFile> listOfFiles, string newLine)
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

                string hyperlink = Hyperlinks.TextToHyperlink(title);

                int qtt = titles[title]++;
                hyperlink = hyperlink.Insert(hyperlink.Length - 1, "-" + qtt.ToString());

                stringBuilder.Append('#', dirNumber + 3);
                stringBuilder.Append(" ");
                stringBuilder.Append(hyperlink);
                stringBuilder.Append(newLine);
            }

            return stringBuilder.ToString();
        }

        private static string CreateTextTOC(List<ISelectedFile> listOfFiles, string newLine)
        {
            StringBuilder tocContent = new StringBuilder();
            List<string> appendedDirectories = new List<string>();
            Queue<ISelectedFile> files = new Queue<ISelectedFile>(listOfFiles);
            // Return if no files on the list
            if (files.Count == 0)
            {
                return newLine;
            }

            ISelectedFile? file = files.Dequeue();
            do
            {
                // Get next file with not null Title
                ISelectedFile? nextFile = null;
                while (files.Count > 0 && nextFile == null)
                {
                    nextFile = files.Dequeue();
                    if (nextFile?.Title == null)
                    {
                        nextFile = null;
                    }
                }

                if (file == null)
                {
                    file = nextFile;
                    continue;
                }

                if (file.Title != null)
                {
                    int headerLvl = 3;

                    // Append subdirectories
                    string[] subdirectories = file.Name?.Split('/', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
                    if (subdirectories.Length > 0 && Path.HasExtension(file.Name))
                    {
                        subdirectories = subdirectories[..^1];
                    }

                    if (nextFile != null && nextFile.MainDirId == file.MainDirId && nextFile.Name != null)
                    {
                        string dirPath = "";
                        foreach (string subdir in subdirectories)
                        {
                            dirPath += "/" + subdir;
                            if (nextFile.Name.Contains(subdir))
                            {
                                if (!appendedDirectories.Contains(dirPath))
                                {
                                    appendedDirectories.Add(dirPath);
                                    tocContent.Append('#', headerLvl);
                                    tocContent.Append(' ');
                                    tocContent.Append(subdir);
                                    tocContent.Append(newLine);
                                }
                                headerLvl++;
                            }
                            else if (appendedDirectories.Contains(dirPath))
            {
                                headerLvl++;
                            }
                            else
                {
                                break;
                            }
                        }
                    }

                    // Append title
                    tocContent.Append('#', headerLvl);
                    tocContent.Append(' ');
                    tocContent.Append(file.Title);
                    tocContent.Append(newLine);
                }

                if (nextFile != null && file.MainDirId != nextFile.MainDirId)
                {
                    appendedDirectories.Clear();
                }

                file = nextFile;
            }
            while (files.Count > 0 || file != null);

            return tocContent.ToString();
        }
    }
}