using System.Text;

namespace MdFilesMerger
{
    internal class MdFile
    {
        public FileInfo FileInfo { get; private set; }
        public string[] SubDirectories { get; private set; }

        public MdFile(FileInfo fileInfo, string mainDirectory)
        {
            FileInfo = fileInfo;
            SubDirectories = GetSubDirectories(mainDirectory);
        }

        public void AppendTo(FileInfo file)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            using (StreamWriter fileStream = file.AppendText())
            {
                using (StreamReader copyFileStream = FileInfo.OpenText())
                {
                    string? line;
                    int prevHeaderLvl = 0;
                    while ((line = copyFileStream.ReadLine()) != null)
                    {
                        // change all headers in file to one level less important (# to ##, ## to ###, h1 to h2, h2 to h3 etc.)
                        
                        // for #*n header format
                        if (line.Trim().StartsWith('#'))
                        {
                            line = "#" + line.TrimStart();
                        }

                        // for <hn>header</hn> format
                        if (prevHeaderLvl != 0)
                        {
                            ChangeHeaderClosingTag(ref line, ref prevHeaderLvl);
                        }

                        if (line.Contains("<h", StringComparison.Ordinal))
                        {
                            int headerLvlStart = line.IndexOf("<h", StringComparison.Ordinal) + 2;
                            int headerLvlEnd = line.IndexOfAny(new char[] { '>', ' ' }, headerLvlStart);

                            if (headerLvlEnd != -1)
                            {
                                if (int.TryParse(line[headerLvlStart..headerLvlEnd], out int HeaderLvl))
                                {
                                    prevHeaderLvl = HeaderLvl;
                                    HeaderLvl++;
                                    line = line.Remove(headerLvlStart, headerLvlEnd - headerLvlStart);
                                    line = line.Insert(headerLvlStart, HeaderLvl.ToString());

                                    ChangeHeaderClosingTag(ref line, ref prevHeaderLvl);
                                }
                            }
                        }

                        fileStream.WriteLine(AdjustHyperlinks(line, this.FileInfo, file));
                    }

                    copyFileStream.Close();
                }

                fileStream.WriteLine();
                fileStream.Close();
            }

            static void ChangeHeaderClosingTag(ref string line, ref int headerLvl)
            {
                if (line.Contains("</h" + headerLvl + ">", StringComparison.Ordinal))
                {
                    var splits = line.Split("</h" + headerLvl + ">", 2);
                    line = splits[0] + "</h" + (headerLvl + 1).ToString() + ">" + splits[1];
                    headerLvl = 0;
                }
            }
        }

        // Open the file and read first not empty line of text from it. If it is a header (starts with '#') return it. Else return filename, without extension.
        // If file header is a link return only text part
        public string GetFileHeader()
        {
            string header = "";
            // as file title
            using (StreamReader streamReader = new StreamReader(FileInfo.OpenRead()))
            {
                header = streamReader.ReadLine() ?? "";
                streamReader.Close();
            }

            // as filename
            if (string.IsNullOrWhiteSpace(header) || header[0] != '#')
            {
                header = FileInfo.Name.Replace(FileInfo.Extension, "");
            }

            return Helpers.HyperlinkToText(header.Trim());
        }

        public string? GetMainDirectoryPath()
        {
            string? mainDirectoryPath = FileInfo.DirectoryName;
            if (mainDirectoryPath != null)
            {
                if (SubDirectories.Length > 0)
                {
                    int index = mainDirectoryPath.IndexOf("\\" + SubDirectories[0] + "\\", StringComparison.Ordinal);

                    if (index == -1)
                    {
                        if (mainDirectoryPath.EndsWith("\\" + SubDirectories[0], StringComparison.Ordinal))
                        {
                            index = mainDirectoryPath.Length - SubDirectories[0].Length - 1;
                        }

                        else if (mainDirectoryPath.StartsWith(SubDirectories[0] + "\\", StringComparison.Ordinal))
                        {
                            index = 0;
                        }

                        else
                        {
                            index = mainDirectoryPath.IndexOf(SubDirectories[0], StringComparison.Ordinal);
                        }
                    }

                    return mainDirectoryPath[..index];
                }
            }

            return mainDirectoryPath;
        }

        private string AdjustHyperlinks(string text, FileInfo sourceFile, FileInfo targetFile)
        {
            int startIndex = 0;
            string substring = text;

            StringBuilder adjustedText = new StringBuilder(text);

            while (Helpers.ContainsHyperlink(substring))
            {
                string link = Helpers.GetLink(substring);

                startIndex = text.IndexOf(link, startIndex, StringComparison.Ordinal);

                // check if not web, absolute or to section link
                if (!(link.StartsWith(@"http://", StringComparison.Ordinal) || link.StartsWith(@"https://", StringComparison.Ordinal) || (char.IsLetter(link[0]) && link[1] == ':') || link[0] == '#') && (sourceFile.DirectoryName != null && targetFile.DirectoryName != null))
                {
                    string absoluteLink = Path.GetFullPath(link, sourceFile.DirectoryName);
                    string newRelativeLink = Path.GetRelativePath(targetFile.DirectoryName, absoluteLink).Replace('\\', '/'); // change to unix style

                    adjustedText.Remove(startIndex, link.Length);
                    adjustedText.Insert(startIndex, newRelativeLink);
                }

                startIndex += link.Length + 1;
                substring = text[startIndex..];
            }

            return adjustedText.ToString();
        }

        private string[] GetSubDirectories(string mainDirectory)
        {
            if (FileInfo.DirectoryName != null)
            {
                string dirs = FileInfo.DirectoryName.Replace(mainDirectory, "");
                var directories = dirs.Split('\\', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                return directories;
            }
            return Array.Empty<string>();
        }
    }
}
