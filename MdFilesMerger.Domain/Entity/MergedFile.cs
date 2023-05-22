using MdFilesMerger.Domain.Abstract;
using MdFilesMerger.Domain.Common;

namespace MdFilesMerger.Domain.Entity
{
    public class MergedFile : MdFile, IMergedFile
    {
        public int UserId { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string? Title { get; set; }
        public MergedFile() : base()
        {
            Title = "Kurs \"Zostań programistą ASP.NET\" - notatki";
            ModifiedDate = DateTime.Now;
        }

        public MergedFile(int id, int mainDirectoryId) : this()
        {
            Id = id;
            MainDirId = mainDirectoryId;
        }

        //public MergedFile(int id, DirectoryInfo? directory, string fileName):this(id)
        //{
        //    Info = new FileInfo(CreatePath(directory?.FullName, fileName));
        //}

        //public MergedFile(int id, string path):this(id)
        //{
        //    if (!path.EndsWith(".md"))
        //    {
        //        path += ".md";
        //    }

        //    Info = new FileInfo(path);
        //}

        //private MergedFile(int id):base(id)
        //{
        //    
        //}

        //public bool SetDirectory(string? path)
        //{
        //    if (string.IsNullOrEmpty(path))
        //    {
        //        return false;
        //    }

        //    else
        //    {
        //        try
        //        {
        //            DirectoryInfo dir = Directory.CreateDirectory(path.ToString());

        //            Info = new FileInfo(CreatePath(dir.FullName, Info.Name));

        //            return true;
        //        }

        //        catch (Exception ex)
        //        {
        //            Console.Error.WriteLine($"{nameof(MergedFile.SetDirectory)} error: {ex} on path: {path}");

        //            return false;
        //        }
        //    }
        //}

        //public bool SetFileName(string? fileName)
        //{
        //    if (string.IsNullOrEmpty(fileName))
        //    {
        //        return false;
        //    }

        //    else
        //    {
        //        Info = new FileInfo(CreatePath(Info.DirectoryName, fileName));
        //        return true;
        //    }
        //}
        //public bool SetTitle(string? title)
        //{
        //    Title = title ?? string.Empty;
        //    return true;
        //}

        //private string CreatePath(string? directoryPath, string? fileName)
        //{
        //    directoryPath = directoryPath?.Trim() ?? string.Empty;
        //    fileName = fileName?.Trim() ?? string.Empty;

        //    if (fileName.Length > 0 && !fileName.EndsWith(".md"))
        //    {
        //        fileName += ".md";
        //    }

        //    return Path.Combine(directoryPath, fileName);
        //}
    }
}
