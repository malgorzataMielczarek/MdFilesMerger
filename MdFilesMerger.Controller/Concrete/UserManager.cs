using MdFilesMerger.App.Concrete;
using MdFilesMerger.Controller.Abstract;
using MdFilesMerger.Controller.Common;
using MdFilesMerger.Domain.Abstract;
using MdFilesMerger.Domain.Entity;

namespace MdFilesMerger.Controller.Concrete
{
    public class UserManager : BaseManager<User, UserService>, ICRUDManager<User, UserService>
    {
        public UserManager() : base(new UserService())
        {
            SelectedItem = 1;
            MergedFileManager = new MergedFileManager();
        }

        public MergedFileManager MergedFileManager { get; }

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

        public bool Delete(int connectedItemId = 0) => Delete();

        public override void DisplayTitle()
        {
            DisplayTitle(SelectedItem switch
            {
                -1 => "Logowanie",
                _ => "Edytuj profil"
            });
        }

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