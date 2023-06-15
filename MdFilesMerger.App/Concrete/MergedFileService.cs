using MdFilesMerger.App.Abstract;
using MdFilesMerger.App.Common;
using MdFilesMerger.Domain.Abstract;
using MdFilesMerger.Domain.Common;
using MdFilesMerger.Domain.Entity;
using System.Security;

namespace MdFilesMerger.App.Concrete
{
    /// <summary>
    ///     Service handling list of <see cref="MergedFile"/> objects associated with the .md files,
    ///     that will be created as a result of merge, whose absolute paths are stored.
    ///     <para>
    ///         <b> Inheritance: </b><see cref="BaseService{T}"/> -&gt; <see cref="BaseDirectoryService{T}"/>
    ///         -&gt; MergedFileService <br/><b>
    ///         Implements: </b><see cref="ICRUDService{T}"/>, <see cref="IDirectoryService{T}"/>,
    ///                     <see cref="IEditFileService{T}"/>, <see cref="IMergedFileService"/>,
    ///                     <see cref="IService{T}"/>
    ///     </para>
    /// </summary>
    /// <seealso cref="ICRUDService{T}"> MdFilesMerger.App.Abstract.ICRUDService&lt;T&gt; </seealso>
    /// <seealso cref="IDirectoryService{T}"> MdFilesMerger.App.Abstract.IDirectoryService&lt;T&gt; </seealso>
    /// <seealso cref="IEditFileService{T}"> MdFilesMerger.App.Abstract.IEditFileService&lt;T&gt; </seealso>
    /// <seealso cref="IMergedFileService"> MdFilesMerger.App.Abstract.IMergedFileService </seealso>
    /// <seealso cref="IService{T}"> MdFilesMerger.App.Abstract.IService&lt;T&gt; </seealso>
    /// <seealso cref="BaseDirectoryService{T}"> MdFilesMerger.App.Common.BaseDirectoryService&lt;T&gt; </seealso>
    /// <seealso cref="BaseService{T}"> MdFilesMerger.App.Common.BaseService&lt;T&gt; </seealso>
    /// <seealso cref="MergedFile"> MdFilesMerger.Domain.Concrete.MergedFile </seealso>
    public sealed class MergedFileService : BaseDirectoryService<MergedFile>, IMergedFileService
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MergedFileService"/> class and fills
        ///     list with example data.
        /// </summary>
        /// <remarks>
        ///     Creates <see cref="MergedFile"/> object associated with
        ///     [MyDocuments]/MergedFiles/README.md file.
        /// </remarks>
        public MergedFileService() : base()
        {
            Initialize();
        }

        /// <inheritdoc/>
        public void AppendTableOfContents(FileInfo? destinationFile, int mergedFileId, List<SelectedFile> selectedFiles)
        {
            if (destinationFile == null || mergedFileId == -1)
            {
                return;
            }

            MergedFile? mergedFile = ReadById(mergedFileId);
            if (mergedFile == null || mergedFile.TableOfContents == TableOfContents.None)
            {
                return;
            }

            using (StreamWriter streamWriter = destinationFile.AppendText())
            {
                // Enter table of contents
                if (!string.IsNullOrWhiteSpace(mergedFile.TOCHeader))
                {
                    streamWriter.WriteLine(mergedFile.TOCHeader);
                }
                streamWriter.WriteLine(TableOfContentsService.CreateTOC(mergedFile, selectedFiles));

                streamWriter.Close();
            }
        }

        /// <inheritdoc/>
        public FileInfo? CreateFile(int id)
        {
            MergedFile? mergedFile = ReadById(id);
            string? mergedFilePath = mergedFile?.GetPath();
            if (string.IsNullOrWhiteSpace(mergedFilePath))
            {
                return null;
            }
            else
            {
                FileInfo mergedFileInfo = new FileInfo(mergedFilePath);

                if (mergedFileInfo.Exists)
                {
                    mergedFileInfo.Delete();
                }

                try
                {
                    using (StreamWriter streamWriter = mergedFileInfo.CreateText())
                    {
                        // set new line format
                        streamWriter.NewLine = mergedFile!.NewLineStyle;

                        // Enter title if exists
                        if (!string.IsNullOrWhiteSpace(mergedFile.Title))
                        {
                            string firstLine = "# " + mergedFile.Title;
                            streamWriter.WriteLine(firstLine);
                        }

                        streamWriter.Close();
                    }
                    return mergedFileInfo;
                }
                catch (Exception ex) when (ex is UnauthorizedAccessException || ex is IOException || ex is SecurityException)
                {
                    return null;
                }
            }
        }

        /// <inheritdoc/>
        public List<MergedFile> ReadByUserId(int userId)
        {
            List<MergedFile> mergedFiles = new List<MergedFile>();
            foreach (var file in _items)
            {
                if (file.UserId == userId)
                {
                    mergedFiles.Add(file);
                }
            }

            return mergedFiles;
        }

        /// <inheritdoc/>
        public int UpdateFileName(int id, string fileName)
        {
            MergedFile? file = ReadById(id);

            if (file != null)
            {
                string? oldPath = file.Name;
                if (file.SetFileName(fileName))
                {
                    file.ModifiedDate = DateTime.Now;

                    return file.Id;
                }
                else
                {
                    file.Name = oldPath;
                }
            }

            return -1;
        }

        /// <inheritdoc/>
        public int UpdateNewLineStyle(int id, string newLineStyle)
        {
            MergedFile? file = ReadById(id);

            if (file != null)
            {
                file.NewLineStyle = newLineStyle;
                file.ModifiedDate = DateTime.Now;

                return file.Id;
            }

            return -1;
        }

        /// <inheritdoc/>
        public int UpdateParentDirectory(int id, string directoryPath)
        {
            MergedFile? file = ReadById(id);

            if (file != null)
            {
                string? oldPath = file.Name;
                if (file.SetParentDirectory(directoryPath))
                {
                    file.ModifiedDate = DateTime.Now;

                    return file.Id;
                }
                else
                {
                    file.Name = oldPath;
                }
            }

            return -1;
        }

        /// <inheritdoc/>
        public int UpdateTableOfContents(int id, TableOfContents tableOfContents)
        {
            MergedFile? file = ReadById(id);

            if (file != null)
            {
                file.TableOfContents = tableOfContents;
                file.ModifiedDate = DateTime.Now;

                return file.Id;
            }

            return -1;
        }

        /// <inheritdoc/>
        public int UpdateTitle(int id, string? title)
        {
            MergedFile? file = ReadById(id);

            if (file != null)
            {
                file.Title = title;
                file.ModifiedDate = DateTime.Now;

                return file.Id;
            }

            return -1;
        }

        /// <inheritdoc/>
        public int UpdateTOCHeader(int id, string? header)
        {
            MergedFile? file = ReadById(id);

            if (file != null)
            {
                if (string.IsNullOrWhiteSpace(header))
                {
                    file.TOCHeader = string.Empty;
                }
                else if (header.StartsWith('#'))
                {
                    file.TOCHeader = header;
                }
                else
                {
                    file.TOCHeader = header;
                }

                file.ModifiedDate = DateTime.Now;

                return file.Id;
            }

            return -1;
        }

        private void Initialize()
        {
            MergedFile mergedFile = new MergedFile(1, 1);
            mergedFile.SetPath(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MergedFiles", "README.md"));
            Create(mergedFile);
        }
    }
}