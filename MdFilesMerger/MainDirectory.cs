using System.Reflection;

namespace MdFilesMerger
{
    internal class MainDirectory
    {
        private DirectoryInfo? _info;

        public DirectoryInfo? Info => _info;

        public MainDirectory()
        {
            string? path = GetDefaultPath();
            SetMainDirectory(path);
        }

        public bool SetMainDirectory(string? path)
        {
            if (Directory.Exists(path))
            {
                _info = new DirectoryInfo(path);

                return true;
            }

            else
            {
                return false;
            }
        }

        private string? GetDefaultPath()
        {
            string appName = Assembly.GetExecutingAssembly().GetName().Name ?? "MdFilesMerger";
            DirectoryInfo dir = new DirectoryInfo(Environment.CurrentDirectory);

            while (dir.Name != appName)
            {
                if (dir.Parent == null)
                {
                    return null;
                }

                else
                {
                    dir = dir.Parent;
                }
            }

            string? parentPath = dir.Parent?.Parent?.FullName;

            if (parentPath != null)
            {
                return Path.Combine(parentPath, "KursZostanProgramistaASPdotNET");
            }

            else
            {
                return null;
            }
        }
    }
}
