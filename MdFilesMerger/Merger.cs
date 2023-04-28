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
            using(FileStream fs = new FileStream(File.FullName, FileMode.Create, FileAccess.Write))
            {
                //Enter title is exist
                if(!string.IsNullOrEmpty(Title))
                {
                    string firstLine = "# " + Title + Program.NEW_LINE;
                    byte[] buf = Encoding.UTF8.GetBytes(firstLine);
                    fs.Write(buf, 0, buf.Length);
                }

                //Enter table of contents if exist
                if(!string.IsNullOrEmpty(TableOfContents))
                {
                    byte[] buf = Encoding.UTF8.GetBytes(TableOfContents);
                    fs.Write(buf, 0, buf.Length);

                    byte[] newLine = Encoding.UTF8.GetBytes(Program.NEW_LINE);
                    fs.Write(newLine, 0, newLine.Length);
                }

                //Enter files content
                if (mdFiles != null)
                {
                    foreach (MdFile file in mdFiles)
                    {
                        using(FileStream copyFileStream = new FileStream(file.FileInfo.FullName, FileMode.Open, FileAccess.Read))
                        {
                            byte[] buf = new byte[1024];
                            int c;
                            while ((c = copyFileStream.Read(buf, 0, buf.Length)) > 0)
                            {
                                fs.Write(buf, 0, c);
                            }
                            copyFileStream.Close();
                        }
                        byte[] newLine = Encoding.UTF8.GetBytes(Program.NEW_LINE);
                        fs.Write(newLine, 0, newLine.Length);
                    }
                }
                fs.Close();
            }
        }

        private readonly string TableOfContents;
        private readonly ListOfMdFiles? mdFiles;
    }
}
