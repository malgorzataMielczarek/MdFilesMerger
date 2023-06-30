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
        /// <param name="mergedFile"> The merged file for which table of content will be created. </param>
        /// <param name="selectedFiles">
        ///     The list of all files, whose content will be placed in the merged file.
        /// </param>
        /// <returns> Text of whole table of contents. </returns>
        public static string? CreateTOC(IMergedFile mergedFile, List<ISelectedFile> selectedFiles) => mergedFile?.TableOfContents switch
        {
            TableOfContents.Text => CreateTocContent(selectedFiles, mergedFile.NewLineStyle, TableOfContents.Text),
            TableOfContents.Hyperlink => CreateTocContent(selectedFiles, mergedFile.NewLineStyle, TableOfContents.Hyperlink),
            _ => null
        };

        private static string? CreateTocContent(List<ISelectedFile> listOfFiles, string newLine, TableOfContents tableOfContents)
        {
            if (listOfFiles == null)
            {
                return null;
            }

            Queue<ISelectedFile> files = new Queue<ISelectedFile>(listOfFiles);
            // Return if no files on the list
            if (files.Count == 0)
            {
                return newLine;
            }

            StringBuilder tocContent = new StringBuilder();
            List<string> appendedDirectories = new List<string>();
            // For hyperlinks TOC
            Dictionary<string, int> includedLinks = new Dictionary<string, int>();
            if (tableOfContents == TableOfContents.Hyperlink)
            {
                foreach (var f in listOfFiles)
                {
                    if (f?.Title != null)
                    {
                        string hyperlink = Hyperlinks.TextToHyperlink(f.Title);
                        string link = Hyperlinks.GetLink(hyperlink);
                        if (includedLinks.ContainsKey(link))
                        {
                            includedLinks[link]++;
                        }
                        else
                        {
                            includedLinks.Add(link, 1);
                        }
                    }
                }
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
                    if (tableOfContents == TableOfContents.Text)
                    {
                        tocContent.Append(file.Title);
                    }
                    else if (tableOfContents == TableOfContents.Hyperlink)
                    {
                        string hyperlink = Hyperlinks.TextToHyperlink(file.Title);
                        string link = Hyperlinks.GetLink(hyperlink);

                        if (!string.IsNullOrEmpty(link))
                        {
                            hyperlink = hyperlink.Insert(hyperlink.Length - 1, "-" + includedLinks[link]++.ToString());
                        }

                        tocContent.Append(hyperlink);
                    }
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