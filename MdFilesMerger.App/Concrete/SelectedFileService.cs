using MdFilesMerger.App.Abstract;
using MdFilesMerger.App.Common;
using MdFilesMerger.Domain.Entity;
using System.Text;

namespace MdFilesMerger.App.Concrete
{
    /// <summary>
    ///     Class handling list of <see cref="SelectedFile"/> objects associated with the .md files
    ///     used during merge, whose paths are stored as relative to connected main directory path.
    ///     <para>
    ///         <b> Inheritance: </b><see cref="BaseService{T}"/> -&gt; <see cref="BaseDirectoryService{T}"/>
    ///         -&gt; <see cref="RelativeFileService{T}"/> -&gt; SelectedFileService <br/><b>
    ///         Implements: </b><see cref="ICRUDService{T}"/>, <see cref="IDirectoryService{T}"/>,
    ///                     <see cref="IEditFileService{T}"/>, <see
    ///                     cref="IRelativeFileService{T}"/>, <see cref="ISelectedFileService"/>,
    ///                     <see cref="IService{T}"/>
    ///     </para>
    /// </summary>
    /// <seealso cref="ICRUDService{T}"> MdFilesMerger.App.Abstract.ICRUDService&lt;T&gt; </seealso>
    /// <seealso cref="IDirectoryService{T}"> MdFilesMerger.App.Abstract.IDirectoryService&lt;T&gt; </seealso>
    /// <seealso cref="IEditFileService{T}"> MdFilesMerger.App.Abstract.IEditFileService&lt;T&gt; </seealso>
    /// <seealso cref="IRelativeFileService{T}"> MdFilesMerger.App.Abstract.IRelativeFileService&lt;T&gt; </seealso>
    /// <seealso cref="ISelectedFileService"> MdFilesMerger.App.Abstract.ISelectedFileService </seealso>
    /// <seealso cref="IService{T}"> MdFilesMerger.App.Abstract.IService&lt;T&gt; </seealso>
    /// <seealso cref="BaseDirectoryService{T}"> MdFilesMerger.App.Common.BaseDirectoryService&lt;T&gt; </seealso>
    /// <seealso cref="BaseService{T}"> MdFilesMerger.App.Common.BaseService&lt;T&gt; </seealso>
    /// <seealso cref="RelativeFileService{T}"> MdFilesMerger.App.Common.RelativeFileService&lt;T&gt; </seealso>
    /// <seealso cref="SelectedFile"> MdFilesMerger.Domain.Entity.SelectedFile </seealso>
    public sealed class SelectedFileService : RelativeFileService<SelectedFile>, ISelectedFileService
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SelectedFileService"/> class.
        /// </summary>
        /// <param name="mainDirectoryService"> </param>
        public SelectedFileService(MainDirectoryService mainDirectoryService) : base(mainDirectoryService)
        {
            Initialize();
        }

        /// <inheritdoc/>
        public void AppendFile(int id, FileInfo? destinationFile, string newLine)
        {
            AppendFile(ReadById(id), destinationFile, newLine);
        }

        /// <inheritdoc/>
        public void AppendFile(SelectedFile? appendedFile, FileInfo? destinationFile, string newLine)
        {
            string? filePath = GetFullPath(appendedFile);
            if (string.IsNullOrWhiteSpace(filePath) || destinationFile == null)
            {
                return;
            }

            FileInfo file = new FileInfo(filePath);
            using (StreamWriter streamWriter = destinationFile.AppendText())
            {
                // set new line format
                streamWriter.NewLine = newLine;

                using (StreamReader copyFileStream = file.OpenText())
                {
                    string? line;
                    int prevHeaderLvl = 0;

                    // Get first not empty line. Omit leading empty lines.
                    while (string.IsNullOrWhiteSpace(line = copyFileStream.ReadLine()))
                    {
                        if (line == null)
                        {
                            break;
                        }
                    }

                    // Enter title if exists
                    if (!string.IsNullOrWhiteSpace(appendedFile!.Title))
                    {
                        string firstLine = "## " + appendedFile.Title;
                        streamWriter.WriteLine(firstLine);

                        if (line != null && Hyperlinks.GetText(line).ToLower() == appendedFile.Title.ToLower())
                        {
                            line = copyFileStream.ReadLine();
                        }
                    }

                    while (line != null)
                    {
                        // change all headers in file to one level less important (# to ##, ## to
                        // ###, h1 to h2, h2 to h3 etc.)

                        // for #*n header format
                        if (line.TrimStart().StartsWith('#'))
                        {
                            line = "#" + line.TrimStart();
                        }

                        // for
                        // <hn> header </hn>
                        // format
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
                                    prevHeaderLvl = HeaderLvl; HeaderLvl++; line = line.Remove(headerLvlStart, headerLvlEnd - headerLvlStart);
                                    line = line.Insert(headerLvlStart, HeaderLvl.ToString());

                                    ChangeHeaderClosingTag(ref line, ref prevHeaderLvl);
                                }
                            }
                        }

                        streamWriter.WriteLine(AdjustHyperlinks(line));

                        line = copyFileStream.ReadLine();
                    }

                    copyFileStream.Close();
                }

                streamWriter.WriteLine();
                streamWriter.Close();
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

            string AdjustHyperlinks(string text)
            {
                int startIndex = 0;
                string substring = text;

                StringBuilder adjustedText = new StringBuilder(text);

                while (Hyperlinks.ContainsHyperlink(substring))
                {
                    string link = Hyperlinks.GetLink(substring);

                    startIndex = text.IndexOf(link, startIndex, StringComparison.Ordinal);

                    // check if not web, absolute or to section link
                    if (!(link.StartsWith(@"http://", StringComparison.Ordinal) || link.StartsWith(@"https://", StringComparison.Ordinal) || (char.IsLetter(link[0]) && link[1] == ':') || link[0] == '#') && (file.DirectoryName != null && destinationFile.DirectoryName != null))
                    {
                        string absoluteLink = Path.GetFullPath(link, file.DirectoryName);
                        string newRelativeLink = Path.GetRelativePath(destinationFile.DirectoryName, absoluteLink).Replace('\\', '/'); // change to unix style

                        adjustedText.Remove(startIndex, link.Length);
                        adjustedText.Insert(startIndex, newRelativeLink);
                    }

                    startIndex += link.Length + 1;
                    substring = text[startIndex..];
                }

                return adjustedText.ToString();
            }
        }

        /// <inheritdoc/>
        public int CreateRange(IEnumerable<FileInfo> list, MainDirectory mainDirectory)
        {
            List<SelectedFile> selectedFiles = new List<SelectedFile>();
            foreach (FileInfo file in list)
            {
                if (!string.IsNullOrWhiteSpace(file?.FullName) && !string.IsNullOrWhiteSpace(mainDirectory?.Name))
                {
                    SelectedFile selectedFile = new SelectedFile(file, mainDirectory);

                    if (selectedFile.Name != null)
                    {
                        selectedFiles.Add(selectedFile);
                    }
                }
            }

            return CreateRange(selectedFiles);
        }

        /// <inheritdoc/>
        public int UpdateTitle(int id, string? title)
        {
            SelectedFile? file = ReadById(id);

            if (file != null)
            {
                file.Title = title;
                file.ModifiedDate = DateTime.Now;

                return file.Id;
            }

            return -1;
        }

        private void Initialize()
        {
            foreach (var mainDir in _mainDirService.ReadAll())
            {
                CreateRange(_mainDirService.FindAllFiles(mainDir), mainDir);
            }
        }
    }
}