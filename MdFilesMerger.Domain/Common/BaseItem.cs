using MdFilesMerger.Domain.Abstract;

namespace MdFilesMerger.Domain.Common
{
    /// <summary>
    /// Implementation of base model for all models
    /// </summary>
    public class BaseItem : IItem
    {
        /// <inheritdoc/>
        public int Id { get; set; }

        /// <inheritdoc/>
        public string? Name { get; set; }

        /// <summary>
        /// Sets <see cref="Id">Id</see> to <see langword="0"/> and <see cref="Name">Name</see> to <see langword="null"/>.
        /// </summary>
        public BaseItem()
        {
            Id = 0;
            Name = null;
        }

        /// <summary>
        /// Sets <see cref="Id">Id</see> to <paramref name="id"/> and <see cref="Name">Name</see> to <see langword="null"/>.
        /// </summary>
        /// <param name="id">Value of <see cref="Id">Id</see>.</param>
        public BaseItem(int id)
        {
            Id = id;
            Name = null;
        }

        /// <summary>
        /// Sets <see cref="Id">Id</see> to <see langword="0"/> and <see cref="Name">Name</see> to <paramref name="name"/>.
        /// </summary>
        /// <param name="name">Value of <see cref="Name">Name</see>.</param>
        public BaseItem(string? name)
        {
            Id = 0;
            Name = name;
        }

        /// <summary>
        /// Sets <see cref="Id">Id</see> to <paramref name="id"/> and <see cref="Name">Name</see> to <paramref name="name"/>.
        /// </summary>
        /// <param name="id">Value of <see cref="Id">Id</see>.</param>
        /// <param name="name">Value of <see cref="Name">Name</see>.</param>
        public BaseItem(int id, string? name)
        {
            Id = id;
            Name = name;
        }
    }
}
