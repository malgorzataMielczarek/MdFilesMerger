using MdFilesMerger.Domain.Abstract;

namespace MdFilesMerger.Domain.Common
{
    public class BaseFile : BaseItem, IFile
    {
        public BaseFile()
        {

        }
        public BaseFile(int id) : base(id)
        {

        }

        public int CompareTo(IFile? other)
        {
            if (other == null)
            {
                return 1;
            }

            string[]? thisSubdir = Path.GetDirectoryName(Name)?.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            string[]? otherSubdir = Path.GetDirectoryName(other.Name)?.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

            int thisSubdirCount = thisSubdir?.Length ?? 0;
            int otherSubdirCount = otherSubdir?.Length ?? 0;

            if (otherSubdirCount == 0)
            {
                if (thisSubdirCount > 0)
                {
                    return 1;
                }

                // If no subdirectories order by file name
                else
                {
                    return CompareFileNames(GetFileName(), other.GetFileName());
                }
            }

            else
            {
                if (thisSubdirCount == 0)
                {
                    return -1;
                }

                // if subdirectories order by subdirectories names and file names
                int minSubDirCount = thisSubdirCount > otherSubdirCount ? otherSubdirCount : thisSubdirCount;

                // order by subdirectories names
                for (int j = 0; j < minSubDirCount; j++)
                {
                    int compare = CompareFileNames(thisSubdir![j], otherSubdir![j]);

                    // if different directory sort by this directory name
                    if (compare != 0)
                    {
                        return compare;
                    }
                }

                // if other has the same subdirectories as this file

                // if this file has more subdirectories then other file it lays lower in directory tree. Change order
                if (thisSubdirCount > otherSubdirCount)
                {
                    return -1;
                }

                // if this file and other file lay directly in the same subdirectory, order by file name
                else if (thisSubdirCount == otherSubdirCount)
                {
                    return CompareFileNames(GetFileName(), other.GetFileName());
                }

                // if other file has more subdirectories then this file, order is correct
                else
                {
                    return 1;
                }
            }
        }

        // If BaseFile is a directory it gets this directory name
        public string? GetFileName()
        {
            return Path.GetFileName(Name);
        }

        public string? GetFullPath()
        {
            return Name?.Replace('/', Path.DirectorySeparatorChar);
        }

        // It gets parent directory path
        public string? GetParentDirectory()
        {
            return Path.GetDirectoryName(Name);
        }

        // If BaseFile is a directory it sets this directory name
        public bool SetFileName(string fileName)
        {
            string directory = GetParentDirectory() ?? string.Empty;

            return SetFullPath(Path.Combine(directory, fileName.Trim()));
        }

        // It sets full path (absolute or relative) of file
        public virtual bool SetFullPath(string? path)
        {
            path = path?.Trim();

            if (string.IsNullOrWhiteSpace(path) || path.Any(c => Path.GetInvalidPathChars().Contains(c)))
            {
                Name = null;
                return false;
            }

            else
            {
                Name = Path.ChangeExtension(path, ".md")?.Replace('\\', '/');
                return true;
            }
        }

        // It sets parent directory path
        public bool SetParentDirectory(string directoryPath)
        {
            string fileName = GetFileName() ?? string.Empty;

            return SetFullPath(Path.Combine(directoryPath.Trim(), fileName));
        }

        /***
         * Compare file names. If names have numbers ends with number check if names are the same except numeric part. 
         * If yes extract numeric part and order names by numeric order.
         * Returns:
         * 0 - if names are the same
         * -1 - if name1 is before name2
         * 1 - if name2 is before name1
         ***/
        private static int CompareFileNames(string? name1, string? name2)
        {
            int compare = string.Compare(name1, name2, StringComparison.Ordinal);
            if (compare == 0)
            {
                return 0;
            }

            else
            {
                int index1 = GetStartOfCounter(name1);
                int index2 = GetStartOfCounter(name2);

                if (index1 >= 0 && index2 >= 0)
                {
                    int number1 = int.Parse(name1![index1..]);
                    int number2 = int.Parse(name2![index2..]);

                    name1 = name1[..index1];
                    name2 = name2[..index2];

                    if (string.Compare(name1, name2) == 0)
                    {
                        if (number1 < number2)
                        {
                            return -1;
                        }

                        else if (number1 > number2)
                        {
                            return 1;
                        }

                        else
                        {
                            return 0;
                        }
                    }
                }

                return compare;
            }

            static int GetStartOfCounter(string? name)
            {
                if (string.IsNullOrEmpty(name))
                {
                    return -1;
                }

                int index = name.Length - 1;

                while (char.IsNumber(name, index))
                {
                    if (index > 0 && char.IsNumber(name, index - 1))
                    {
                        index--;
                    }

                    else
                    {
                        return index;
                    }
                }

                return -1;
            }
        }
    }
}
