using BusinessLogic.Library.Authentication;

namespace ConsoleApp.Library
{
    internal class Application : IRunnable
    {
        private Session _session;
        private readonly Menu _menu;
        private readonly MenuUtils _menuUtils;

        public Application(Menu menu, MenuUtils menuUtils)
        {
            _session = Session.GetInstance();
            _menu = menu;
            _menuUtils = menuUtils;
        }

        public void Run()
        {
            Console.WriteLine("Welcome in our library.\n");

            if (!_menu.StartMenu())
            {
                Close();
            }

            bool loginSuccess;
            do
            {
                LoginRecord record = _menu.LoginMenu();
                loginSuccess = _session.Login(record.Username, record.Password);
                if (!loginSuccess)
                {
                    Console.WriteLine("Invalid login. Retry?");
                    Console.WriteLine("1. Yes");
                    Console.WriteLine("2. No");

                    string res = _menuUtils.GetStrictInteraction("Insert command", input => input != "1" && input != "2" || input == string.Empty);
                    if (res == "2")
                    {
                        Close();
                    }
                }
            } while (!loginSuccess);

            if(_session.IsAdmin)
            {
                Console.WriteLine("Admin menu");
            }
            else
            {
                Console.WriteLine("Basic user menu");
            }
        }

        public void Close()
        {
            _session.Logout();
            Environment.Exit(0);
        }
    }
}
