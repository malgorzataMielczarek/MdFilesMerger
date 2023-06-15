using MdFilesMerger.Domain.Abstract;

namespace MdFilesMerger.Domain.Common
{
    /// <summary>
    ///     Implementation of base model for all models.
    ///     <para> <b> Inheritance: </b> BaseItem <br/><b> Implements: </b><see cref="IItem"/> </para>
    /// </summary>
    /// <seealso cref="IItem"> MdFilesMerger.Domain.Abstract.IItem </seealso>
    public class BaseItem : IItem
    {
        /// <summary>
        ///     Sets all properties to its default values.
        /// </summary>
        public BaseItem()
        {
            Id = 0;
            Name = null;
        }

        /// <summary>
        ///     Sets <see cref="Id"> Id </see> to <paramref name="id"/> and rest properties to its
        ///     default value.
        /// </summary>
        /// <param name="id"> This item identification number. </param>
        public BaseItem(int id)
        {
            Id = id;
            Name = null;
        }

        /// <summary>
        ///     Sets <see cref="Name"> Name </see> to <paramref name="name"/> and rest properties to
        ///     its default values.
        /// </summary>
        /// <param name="name"> This item description. </param>
        public BaseItem(string? name)
        {
            Id = 0;
            Name = name;
        }

        /// <summary>
        ///     Sets <see cref="Id"> Id </see> to <paramref name="id"/> and <see cref="Name"> Name
        ///     </see> to <paramref name="name"/>.
        /// </summary>
        /// <param name="id"> This item identification number. </param>
        /// <param name="name"> This item description. </param>
        public BaseItem(int id, string? name)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        ///     <inheritdoc/>
        /// </summary>
        /// <value> By default set to <see langword="0"/>. </value>
        public int Id { get; set; }

        /// <summary>
        ///     <inheritdoc/>
        /// </summary>
        /// <value> By default set to <see langword="null"/>. </value>
        public string? Name { get; set; }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return Id;
        }
    }
}