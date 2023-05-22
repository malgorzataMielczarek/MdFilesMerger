using MdFilesMerger.Domain.Abstract;
using MdFilesMerger.Domain.Common;

namespace MdFilesMerger.Domain.Entity
{
    public class User : BaseItem, IUser
    {
        public string? Password { get; set; }

        public User()
        {

        }

        public User(int id) : base(id)
        {

        }

        public User(int id, string userName, string password) : base(id, userName)
        {
            Password = password;
        }
    }
}
