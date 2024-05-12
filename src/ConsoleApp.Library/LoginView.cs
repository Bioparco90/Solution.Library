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
