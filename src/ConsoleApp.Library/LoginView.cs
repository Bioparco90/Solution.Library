namespace ConsoleApp.Library
{
    internal class LoginView
    {
        private readonly MenuUtils _menuUtils;

        public LoginView(MenuUtils menuUtils)
        {
            _menuUtils = menuUtils;
        }

        private bool CheckEmpty(string input)
        {
            return input == string.Empty;
        }

        public LoginRecord AskLogin()
        {
            string username = _menuUtils.GetStrictInteraction("Username", CheckEmpty);
            string password = _menuUtils.GetStrictInteraction("Password", CheckEmpty, ReadPassword);

            return new(username, password);
        }

        public string AskRetry()
        {
            Console.WriteLine("Invalid login. Retry?");
            Console.WriteLine("1. Yes");
            Console.WriteLine("2. No");

            return _menuUtils.GetStrictInteraction("Insert command", input => input != "1" && input != "2" || input == string.Empty);
        }

        private string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true); // true per non mostrare il tasto premuto
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*"); // Visualizza un carattere di oscuramento
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Remove(password.Length - 1);
                    Console.Write("\b \b"); // Cancella l'ultimo carattere visualizzato
                }
            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return password;
        }
    }
}
