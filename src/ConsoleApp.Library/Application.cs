using BusinessLogic.Library.Authentication;

namespace ConsoleApp.Library
{
    internal class Application : IRunnable
    {
        private Session _session;
        private Menu _menu;

        public Application(Menu menu)
        {
            _session = Session.GetInstance();
            _menu = menu;
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
                    string res = _menu.Utils.GetStrictInteraction("1. Yes\n2. No", input => input != "1" && input != "2" || input == string.Empty);
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
