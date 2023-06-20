using MdFilesMerger.Domain.Abstract;
using System.Diagnostics.CodeAnalysis;

namespace MdFilesMerger.Domain.Common
{
    /// <summary>
    ///     Base implementation for all directory and file related models.
    ///     <para>
    ///         <b> Inheritance: </b><see cref="BaseItem"/> -&gt; BaseDirectory <br/><b> Implements:
    ///         </b><see cref="IComparable{IDirectory}"/>, <see cref="IDirectory"/>, <see cref="IItem"/>
    ///     </para>
    /// </summary>
    /// <seealso cref="BaseItem"> MdFilesMerger.Domain.Common.BaseItem </seealso>
    /// <seealso cref="IComparable{T}"> System.IComparable&lt;T&gt; </seealso>
    /// <seealso cref="IDirectory"> MdFilesMerger.Domain.Abstract.IDirectory </seealso>
    /// <seealso cref="IItem"> MdFilesMerger.Domain.Abstract.IItem </seealso>
    public class BaseDirectory : BaseItem, IDirectory
    {
        /// <inheritdoc/>
        public BaseDirectory() : base()
        {
            ModifiedDate = DateTime.Now;
        }

        /// <inheritdoc/>
        public BaseDirectory(int id) : base(id)
        {
            ModifiedDate = DateTime.Now;
        }

        /// <summary>
        ///     Sets <see cref="BaseItem.Name"/> to adjusted <paramref name="path"/> and rest
        ///     properties to its default values.
        /// </summary>
        /// <remarks>
        ///     If <paramref name="path"/> is a valid path to directory (file), <see
        ///     cref="BaseItem.Name"/> is set to path to this directory (file), that uses <see
        ///     langword="'/'"/> as directory separator character. Otherwise <see
        ///     cref="BaseItem.Name"/> is set to <see langword="null"/>.
        /// </remarks>
        /// <param name="path">
        ///     Path to the directory (file) that you want to associate with this object.
        /// </param>
        public BaseDirectory(string? path) : base(path)
        {
            ModifiedDate = DateTime.Now;
            SetPath(path);
        }

        /// <summary>
        ///     Sets <see cref="BaseItem.Id"/> to <paramref name="id"/>, <see cref="BaseItem.Name"/>
        ///     to adjusted <paramref name="path"/> and <see cref="ModifiedDate"/> to <see cref="DateTime.Now"/>.
        /// </summary>
        /// <remarks>
        ///     If <paramref name="path"/> is a valid path to directory (file), <see
        ///     cref="BaseItem.Name"/> is set to path to this directory (file), that uses <see
        ///     langword="'/'"/> as directory separator character. Otherwise <see
        ///     cref="BaseItem.Name"/> is set to <see langword="null"/>.
        /// </remarks>
        /// <param name="id"> This item identification number. </param>
        /// <param name="path">
        ///     Path to the directory (file) that you want to associate with this object.
        /// </param>
        public BaseDirectory(int id, string? path) : base(id, path)
        {
            ModifiedDate = DateTime.Now;
            SetPath(path);
        }

        /// <summary>
        ///     Sets all properties to values of appropriate arguments.
        /// </summary>
        /// <remarks>
        ///     No evaluation or adjusting is performed, so use this method only for already
        ///     evaluated and prepared data, for example from database.
        /// </remarks>
        /// <param name="id"> Identification number of item. </param>
        /// <param name="path">
        ///     Valid path to directory/file associated with this item. Path should use <see
        ///     langword="'/'"/> as directory separator.
        /// </param>
        /// <param name="modifiedDate"> Date and time of last modification of this entity. </param>
        public BaseDirectory(int id, string? path, DateTime modifiedDate) : base(id, path)
        {
            ModifiedDate = modifiedDate;
        }

        /// <summary>
        ///     <inheritdoc/>
        /// </summary>
        /// <value> By default it is set to <see cref="DateTime.Now"/>. </value>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        ///     Compares paths ( <see cref="BaseItem.Name"/>) of the current instance and another
        ///     <see cref="BaseDirectory"/> object and returns integer indicating whether current
        ///     instance precedes, follows or occurs in the same position in sort order as <paramref
        ///     name="other"/> object.
        /// </summary>
        /// <remarks>
        ///     After cutting off common part of both paths (common base directory), in sort order
        ///     first occurs object without subdirectories. If both shortened paths (don't)have
        ///     subdirectories they are sorted by names of the leased nested, not common, directory
        ///     (file without extension) in almost lexical order. The difference is, that if both
        ///     names are the same, except for the numerical postfixes, they are ordered numerically
        ///     by values of this postfixes.
        /// </remarks>
        /// <param name="other"> <inheritdoc/> </param>
        /// <returns> <inheritdoc/> </returns>
        public int CompareTo(IDirectory? other)
        {
            if (other?.Name == null)
            {
                return 1;
            }

            if (this.Name == null)
            {
                return -1;
            }

            // cutoff common root (base directory)
            string thisName = Path.GetRelativePath(other.Name, this.Name).Replace(".." + Path.DirectorySeparatorChar, "");
            string otherName = Path.GetRelativePath(this.Name, other.Name).Replace(".." + Path.DirectorySeparatorChar, "");

            string[]? thisSubdir = Path.GetDirectoryName(thisName)?.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            string[]? otherSubdir = Path.GetDirectoryName(otherName)?.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

            int thisSubdirCount = thisSubdir?.Length ?? 0;
            int otherSubdirCount = otherSubdir?.Length ?? 0;

            if (otherSubdirCount == 0)
            {
                if (thisSubdirCount > 0)
                {
                    return 1;
                }

                // If no subdirectories order by name
                else
                {
                    string? thisDirName = Path.GetFileNameWithoutExtension(this.Name);
                    string? otherDirName = Path.GetFileNameWithoutExtension(other.Name);

                    return CompareDirectoriesNames(thisDirName, otherDirName);
                }
            }
            else
            {
                if (thisSubdirCount == 0)
                {
                    return -1;
                }

                // if subdirectories order by subdirectories names and directories (files) names
                int minSubDirCount = thisSubdirCount > otherSubdirCount ? otherSubdirCount : thisSubdirCount;

                // order by subdirectories names
                for (int j = 0; j < minSubDirCount; j++)
                {
                    int compare = CompareDirectoriesNames(thisSubdir![j], otherSubdir![j]);

                    // if different directory sort by this directory name
                    if (compare != 0)
                    {
                        return compare;
                    }
                }

                // if other has the same subdirectories as this one

                // if this directory (file) has more subdirectories then the other it lays lower in
                // the directory tree. Change order
                if (thisSubdirCount > otherSubdirCount)
                {
                    return -1;
                }

                // if this directory (file) and the other lay directly in the same subdirectory,
                // order by name
                else if (thisSubdirCount == otherSubdirCount)
                {
                    string? thisDirName = Path.GetFileNameWithoutExtension(this.Name);
                    string? otherDirName = Path.GetFileNameWithoutExtension(other.Name);

                    return CompareDirectoriesNames(thisDirName, otherDirName);
                }

                // if the other has more subdirectories then this one, order is correct
                else
                {
                    return 1;
                }
            }
        }

        /// <summary>
        ///     Gets directory/file path held in <see cref="BaseItem.Name"/> property.
        /// </summary>
        /// <returns> System specific valid path of this item. </returns>
        [return: NotNullIfNotNull(nameof(Name))]
        public virtual string? GetPath()
        {
            return Name?.Replace('/', Path.DirectorySeparatorChar);
        }

        /// <summary>
        ///     Sets <see cref="BaseItem.Name"/> as adjusted <paramref name="path"/>.
        /// </summary>
        /// <remarks>
        ///     Checks if given <paramref name="path"/> is valid (doesn't contain any invalid path
        ///     characters). If it is valid, sets <see cref="BaseItem.Name"/> as given <paramref
        ///     name="path"/> using <see langword="'/'"/> as directory separator character.
        ///     Otherwise it sets <see cref="BaseItem.Name"/> to <see langword="null"/>.
        /// </remarks>
        /// <param name="path">
        ///     Valid absolute or relative path, that you want this directory(file) to have.
        /// </param>
        /// <returns>
        ///     <list type="bullet">
        ///         <item>
        ///             <see langword="true"/> if <see cref="BaseItem.Name"/> was successfully set.
        ///         </item>
        ///         <item>
        ///             <see langword="false"/> if <see cref="BaseItem.Name"/> was set to <see langword="null"/>
        ///         </item>
        ///     </list>
        /// </returns>
        public virtual bool SetPath([NotNullWhen(true)] string? path)
        {
            path = path?.Trim();

            if (string.IsNullOrWhiteSpace(path) || path.Any(c => Path.GetInvalidPathChars().Contains(c)))
            {
                Name = null;
                return false;
            }
            else
            {
                Name = path.Replace('\\', '/');
                ModifiedDate = DateTime.Now;
                return true;
            }
        }

        /// <summary>
        ///     Compare names of directories (files).
        /// </summary>
        /// <remarks>
        ///     If names end with numbers check if names are the same except for numeric part. If
        ///     yes extract numeric part and sort names by numeric order. Otherwise sort by ordinary
        ///     lexical order.
        /// </remarks>
        /// <param name="name1"> Name of first directory (file). </param>
        /// <param name="name2"> Name of second directory (file). </param>
        /// <returns>
        ///     <list type="table">
        ///         <listheader> <b> Value - when occurs </b> </listheader>
        ///         <item>
        ///             <b> 0 </b> - if names ( <paramref name="name1"/> and <paramref
        ///             name="name2"/>) are the same
        ///         </item>
        ///         <item> <b> -1 </b> - if <paramref name="name1"/> is before <paramref name="name2"/> </item>
        ///         <item> <b> 1 </b> - if <paramref name="name2"/> is before <paramref name="name1"/> </item>
        ///     </list>
        /// </returns>
        private static int CompareDirectoriesNames(string? name1, string? name2)
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