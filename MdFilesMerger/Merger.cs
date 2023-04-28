using System.Reflection.PortableExecutable;
using System.Text;

namespace MdFilesMerger
{
    internal class Merger
    {
        public FileInfo File { get; private set; }
        public string Title { get; set; }

        public Merger(ListOfMdFiles? listOfFiles, string fileName = "README", string? directory = null)
        {
            Title = Program.MERGED_FILE_TITLE;
            TableOfContents = string.Empty;
            mdFiles = listOfFiles;
            if (directory == null)
            {
                if (mdFiles != null && mdFiles.Count > 0)
                    directory = mdFiles.First().GetMainDirectoryPath();
                else directory = Program.MAIN_DIRECTORY_PATH;
            }
            if (string.IsNullOrEmpty(directory)) directory = "..";

            if (string.IsNullOrEmpty(fileName)) fileName = "README";
            if (!fileName.EndsWith(".md")) fileName += ".md";

            File = new FileInfo(directory + "\\" + fileName);

            //If file that will contain marged files is on the list of files to merge
            if(mdFiles != null && mdFiles.Count > 0 && File.Exists && mdFiles.Where(mdFile => mdFile.FileInfo.FullName == File.FullName).Any())
            {
                MdFile mdFile = mdFiles.First(mdFl => mdFl.FileInfo.FullName == File.FullName);
                mdFiles.Remove(mdFile);
            }
        }
        public Merger(TableOfContents? tableOfContents, string fileName = "README", string? directory = null):this(tableOfContents?.ListOfFiles, fileName, directory)
        {
            TableOfContents = tableOfContents?.GetText() ?? string.Empty;
            //If table of contents exist recreate if file was deleted from list in the above constructor
            if (tableOfContents != null && mdFiles != null && tableOfContents.ListOfFiles.Count != mdFiles.Count)
            {
                var tableOfContentsType = tableOfContents.Type;
                tableOfContents = new TableOfContents(mdFiles) { Type = tableOfContentsType };
                TableOfContents = tableOfContents.GetText();
            }
        }
        public Merger(List<MdFile>? listOfFiles, string fileName = "README", string? directory = null):this((ListOfMdFiles?)listOfFiles, fileName, directory)
        {

        }

        public void MergeFiles()
        {
            if(File.Exists) File.Delete();
            using (StreamWriter streamWriter = File.CreateText())
            {
                streamWriter.NewLine = Program.NEW_LINE;
                //Enter title if exists
                if (!string.IsNullOrEmpty(Title))
                {
                    string firstLine = "# " + Title;
                    streamWriter.WriteLine(firstLine);
                }

                //Enter table of contents if exists
                if (!string.IsNullOrEmpty(TableOfContents))
                {
                    streamWriter.WriteLine(TableOfContents);
                }

                //Enter files content
                if (mdFiles != null)
                {
                    foreach (MdFile file in mdFiles)
                    {
                        file.CopyToOpenStreamWriter(streamWriter);
                    }
                }
                streamWriter.Close();
            }
        }

        private readonly string TableOfContents;
        private readonly ListOfMdFiles? mdFiles;
    }
}
