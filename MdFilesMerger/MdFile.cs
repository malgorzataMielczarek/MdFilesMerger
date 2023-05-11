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
        //Open the file and read first not empty line of text from it. If it is a header (starts with '#') return it. Else return file name, without extension.
        //If file header is a link return only text part
        public string GetFileHeader()
        {
            string header = "";
            using(StreamReader streamReader = new StreamReader(FileInfo.OpenRead()))
            {
                header = streamReader.ReadLine() ?? "";
                streamReader.Close();
            }
            header = header.Trim();
            if (header.Length == 0 || header[0] != '#')
                header = FileInfo.Name.Replace(FileInfo.Extension, "");

            return Helpers.ConvertHyperlinkHeaderToTextHeader(header);
        }
        public string? GetMainDirectoryPath()
        {
            string? mainDirectoryPath = FileInfo.DirectoryName;
            if (mainDirectoryPath != null)
            {
                if (SubDirectories.Length > 0)
                {
                    int index = mainDirectoryPath.IndexOf("\\" + SubDirectories[0] + "\\");
                    if (index == -1 && mainDirectoryPath.EndsWith("\\" + SubDirectories[0])) 
                        index = mainDirectoryPath.LastIndexOf("\\" + SubDirectories[0]);
                    if (index == -1 && mainDirectoryPath.StartsWith(SubDirectories[0] + "\\"))
                        index = 0;
                    if (index == -1) index = mainDirectoryPath.IndexOf(SubDirectories[0]);
                    mainDirectoryPath = mainDirectoryPath.Remove(index);
                }
            }
            return mainDirectoryPath;
        }
        public void AppendTo(FileInfo file)
        {
            if(file == null) throw new ArgumentNullException(nameof(file));

            using (StreamWriter fileStream = file.AppendText())
            {
                using (StreamReader copyFileStream = FileInfo.OpenText())
                {
                    string? line;
                    int findHeaderClosingTag = 0;
                    while ((line = copyFileStream.ReadLine()) != null)
                    {
                        //change all headers in file to one level less important (# to ##, ## to ###, h1 to h2, h2 to h3 etc.)
                        //  for #*n header format
                        if (line.Trim().StartsWith('#'))
                        {
                            line = "#" + line.TrimStart();
                        }
                        //  for <hn>header</hn> format
                        if (findHeaderClosingTag != 0) ChangeHeaderClosingTag(ref line, ref findHeaderClosingTag);
                        if (line.Contains("<h"))
                        {
                            int headerLevelStartIndex = line.IndexOf("<h") + 2;
                            int headerLevelEndIndex = line.IndexOfAny(new char[] { '>', ' ' }, headerLevelStartIndex);

                            if (headerLevelEndIndex != -1)
                            {
                                if (int.TryParse(line[headerLevelStartIndex..headerLevelEndIndex], out int headerLevel))
                                {
                                    findHeaderClosingTag = headerLevel;
                                    headerLevel++;
                                    line = line.Remove(headerLevelStartIndex, headerLevelEndIndex - headerLevelStartIndex);
                                    line = line.Insert(headerLevelStartIndex, headerLevel.ToString());

                                    ChangeHeaderClosingTag(ref line, ref findHeaderClosingTag);
                                }
                            }
                        }

                        fileStream.WriteLine(AdjustRelativeLinksInText(line, this.FileInfo, file));
                    }

                    copyFileStream.Close();
                }
                fileStream.WriteLine();

                fileStream.Close();
            }
            static void ChangeHeaderClosingTag(ref string line, ref int findHeaderClosingTag)
            {
                if (line.Contains("</h" + findHeaderClosingTag + ">"))
                {
                    var splits = line.Split("</h" + findHeaderClosingTag + ">", 2);
                    line = splits[0] + "</h" + (findHeaderClosingTag + 1).ToString() + ">" + splits[1];
                    findHeaderClosingTag = 0;
                }
            }
        }
        public string AdjustRelativeLinksInText(string textLine, FileInfo sourceFile, FileInfo targetFile)
        {
            int startIndex = 0;
            string substring = textLine;

            StringBuilder adjustedText = new StringBuilder(textLine);

            while (Helpers.ContainsLinkBlock(substring))
            {
                string link = Helpers.GetLinkPartFromLinkBlock(substring);

                startIndex = textLine.IndexOf(link, startIndex);

                //check if not web, absolute or to section link
                if (!(link.StartsWith(@"http://") || link.StartsWith(@"https://") || (char.IsLetter(link[0]) && link[1] == ':') || link[0] == '#')
                 && (sourceFile.DirectoryName != null && targetFile.DirectoryName != null))
                {
                    string absoluteLink = Path.GetFullPath(link, sourceFile.DirectoryName);
                    string newRelativeLink = Path.GetRelativePath(targetFile.DirectoryName, absoluteLink).Replace('\\', '/'); //change to unix style

                    adjustedText.Remove(startIndex, link.Length);
                    adjustedText.Insert(startIndex, newRelativeLink);
                }

                startIndex += link.Length + 1;
                substring = textLine[startIndex..];
            }
            
            return adjustedText.ToString();
        }
    }
}
