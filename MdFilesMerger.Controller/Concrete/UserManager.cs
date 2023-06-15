using MdFilesMerger.App.Concrete;
using MdFilesMerger.Controller.Abstract;
using MdFilesMerger.Controller.Common;
using MdFilesMerger.Domain.Entity;

namespace MdFilesMerger.Controller.Concrete
{
    /// <summary>
    ///     Manager for user model.
    ///     <para>
    ///         <b> Inheritance: </b><see cref="BaseManager{T, TService}"/> -&gt; UserManager <br/><b>
    ///         Implements: </b><see cref="ICRUDManager{T, TService}"/>, <see cref="IManager{T,
    ///                     TService}"/>, <see cref="IUserManager"/>
    ///     </para>
    /// </summary>
    /// <seealso cref="UserService"> MdFilesMerger.App.Concrete.UserService </seealso>
    /// <seealso cref="ICRUDManager{T, TService}">
    ///     MdFilesMerger.Controller.Abstract.ICRUDManager&lt;T, TService&gt;
    /// </seealso>
    /// <seealso cref="IManager{T, TService}">
    ///     MdFilesMerger.Controller.Abstract.IManager&lt;T, TService&gt;
    /// </seealso>
    /// <seealso cref="IUserManager"> MdFilesMerger.Controller.Abstract.IUserManager </seealso>
    /// <seealso cref="BaseManager{T, TService}">
    ///     MdFilesMerger.Controller.Common.BaseManager&lt;T, TService&gt;
    /// </seealso>
    /// <seealso cref="User"> MdFilesMerger.Domain.Entity.User </seealso>
    public class UserManager : BaseManager<User, UserService>, IUserManager
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="UserManager"/> class and all it's properties.
        /// </summary>
        /// <remarks>
        ///     Sets <see cref="BaseManager{T, TService}.Service"/> to the newly created <see
        ///     cref="UserService"/> object, <see cref="BaseManager{T, TService}.SelectedItem"/> to
        ///     first object of service's collection and <see cref="MergedFileManager"/> to the
        ///     newly created <see cref="Concrete.MergedFileManager"/> object.
        /// </remarks>
        public UserManager() : base(new UserService())
        {
            SelectedItem = 1;
            MergedFileManager = new MergedFileManager();
        }

        /// <inheritdoc/>
        public MergedFileManager MergedFileManager { get; }

        /// <summary>
        ///     Asks user for login and password and creates new user based on the input.
        /// </summary>
        /// <remarks> Created user is added to collection and selected item is set to it. </remarks>
        /// <param name="connectedItemId">
        ///     The value of this parameter doesn't matter in this implementation. It has set
        ///     default value, so it can be omitted all together.
        /// </param>
        /// <returns> <inheritdoc/> </returns>
        public bool Create(int connectedItemId = 0)
        {
            User user = new User();
            string? pwd, pwd2;
            var cursor = Console.GetCursorPosition();
            while (true)
            {
                user.Name = GetNotEmptyLogin(cursor);
                int id = Service.Create(user);

                if (id != -1)
                {
                    SelectedItem = id;
                    break;
                }

                Console.SetCursorPosition(cursor.Left, cursor.Top);
                Console.WriteLine("Użytkownik o podanym loginie już istnieje. Podaj inny login.");
            }

            cursor = Console.GetCursorPosition();
            while (true)
            {
                pwd = GetNotEmptyPwd(cursor);

                var cursor2 = Console.GetCursorPosition();
                Console.Write("Powtórz hasło: ");
                while (string.IsNullOrWhiteSpace(pwd2 = Console.ReadLine()))
                {
                    Console.SetCursorPosition(cursor2.Left, cursor2.Top);
                    Console.Write("Powtórz hasło ponownie: ");
                }

                if (pwd == pwd2)
                {
                    Service.ReadById(SelectedItem)!.SetPassword(pwd);
                    break;
                }

                Console.SetCursorPosition(cursor.Left, cursor.Top);
                Console.WriteLine("Hasło nie zgadza się z powtórzonym hasłem. Podaj hasła ponownie.");
            }

            return true;
        }

        /// <inheritdoc/>
        public bool Delete()
        {
            bool result = MergedFileManager.Delete(SelectedItem);

            if (result)
            {
                result = Service.Delete(SelectedItem) == SelectedItem;

                if (result)
                {
                    SelectedItem = -1;
                }
            }

            return result;
        }

        /// <summary>
        ///     Deletes from the service's collection selected item and all elements of other models
        ///     connected with it.
        /// </summary>
        /// <remarks> It calls <see cref="Delete()"/> method. </remarks>
        /// <param name="connectedItemId">
        ///     The value of this parameter doesn't matter in this implementation. It has set
        ///     default value, so it can be omitted all together.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if all elements was successfully deleted; otherwise <see langword="false"/>.
        /// </returns>
        public bool Delete(int connectedItemId = 0) => Delete();

        /// <inheritdoc/>
        public override void DisplayTitle()
        {
            DisplayTitle(SelectedItem switch
            {
                -1 => "Logowanie",
                _ => "Edytuj profil"
            });
        }

        /// <summary>
        ///     Log in method.
        /// </summary>
        /// <remarks>
        ///     Asks about user's login and password and select appropriate user based on input.
        /// </remarks>
        /// <param name="connectedItemId">
        ///     The value of this parameter doesn't matter in this implementation. It has set
        ///     default value, so it can be omitted all together.
        /// </param>
        public override void Select(int connectedItemId = 0)
        {
            var cursor = Console.GetCursorPosition();
            while (true)
            {
                string login = GetNotEmptyLogin(cursor);
                string? password = GetNotEmptyPwd(Console.GetCursorPosition());
                SelectedItem = Service.CheckCredentials(login, password);
                if (SelectedItem == -1)
                {
                    Console.SetCursorPosition(cursor.Left, cursor.Top);
                    Console.WriteLine("Podano nieprawidłowy login lub hasło. Spróbuj zalogować się ponownie.");
                }
                else
                {
                    break;
                }
            }
        }

        /// <inheritdoc/>
        public bool UpdatePassword()
        {
            User? user;
            if (SelectedItem != -1 && (user = Service.ReadById(SelectedItem)) != null)
            {
                Console.Write("Login: ");
                DisplayItem(user);
                Console.WriteLine("Podaj obecne hasło: ");

                for (int trial = 4; trial >= 0 && !user.PasswordEquals(Console.ReadLine()); trial--)
                {
                    (_, int top) = Console.GetCursorPosition();
                    Console.SetCursorPosition(0, top - 1);

                    char suffix = trial switch
                    {
                        0 => 'o',
                        1 => 'a',
                        _ => 'y'
                    };
                    string suffix2 = trial switch
                    {
                        0 => string.Empty,
                        _ => suffix.ToString()
                    };

                    Console.WriteLine($"Podano nieprawidłowe hasło. Pozostał{suffix} {trial} prób{suffix2}.");

                    if (trial == 0)
                    {
                        Console.WriteLine("Następuje wylogowanie użytkownika.");
                        SelectedItem = -1;
                        return false;
                    }

                    Console.Write("Podaj obacne hasło: ");
                }

                string? newPwd, pwd2;
                var cursor = Console.GetCursorPosition();
                while (true)
                {
                    Console.Write("Nowe hasło: ");
                    while (string.IsNullOrWhiteSpace(newPwd = Console.ReadLine()))
                    {
                        Console.SetCursorPosition(cursor.Left, cursor.Top);
                        Console.WriteLine("Hasło nie może być puste.");
                        Console.Write("Podaj ponownie nowe hasło: ");
                    }

                    var cursor2 = Console.GetCursorPosition();
                    Console.Write("Powtórz hasło: ");
                    while (string.IsNullOrWhiteSpace(pwd2 = Console.ReadLine()))
                    {
                        Console.SetCursorPosition(cursor2.Left, cursor2.Top);
                        Console.WriteLine("Hasło nie może być puste.");
                        Console.Write("Powtórz hasło ponownie: ");
                    }

                    if (newPwd == pwd2)
                    {
                        return Service.UpdatePassword(SelectedItem, newPwd) != -1;
                    }

                    Console.SetCursorPosition(cursor.Left, cursor.Top);
                    Console.WriteLine("Nowe hasło nie zgadza się z powtórzonym hasłem. Podaj hasła ponownie.");
                }
            }

            return false;
        }

        /// <summary>
        ///     Gets all stored users.
        /// </summary>
        /// <param name="connectedItemId">
        ///     The value of this parameter doesn't matter in this implementation. It has set
        ///     default value, so it can be omitted all together.
        /// </param>
        /// <returns> List of all users. </returns>
        protected override List<User> GetFilteredList(int connectedItemId = 0) => Service.ReadAll();

        private string GetNotEmptyLogin((int Left, int Top) cursor)
        {
            string? login;
            Console.Write("Login: ");
            while (string.IsNullOrWhiteSpace(login = Console.ReadLine()))
            {
                Console.SetCursorPosition(cursor.Left, cursor.Top);
                Console.WriteLine("Login nie może być pusty.");
                Console.Write("Podaj ponownie login: ");
            }

            return login;
        }

        private string GetNotEmptyPwd((int Left, int Top) cursor)
        {
            string? pwd;
            Console.Write("Hasło: ");
            while (string.IsNullOrWhiteSpace(pwd = Console.ReadLine()))
            {
                Console.SetCursorPosition(cursor.Left, cursor.Top);
                Console.WriteLine("Hasło nie może być puste.");
                Console.Write("Podaj ponownie hasło: ");
            }

            return pwd;
        }
    }
}