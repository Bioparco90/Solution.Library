using BusinessLogic.Library.V1.Authentication;

namespace ConsoleApp.Library
{
    internal class Menu
    {
        private Utils _utils;
        private LoginView _loginView;

        public Menu(Utils utils, LoginView loginView)
        {
            _utils = utils;
            _loginView = loginView;
        }

        public bool StartMenu()
        {
            Console.WriteLine("Welcome in our library.\n");

            Console.WriteLine("1. Login");
            Console.WriteLine("2. Exit");

            string choice = _utils.GetStrictInteraction("Insert command", input => _utils.CheckInputChoice(input, 2));
            return choice == "1";
        }

        public LoginRecord LoginMenu(Session session)
        {
            LoginRecord record;
            bool loginSuccess;
            do
            {
                record = _loginView.AskLogin();
                loginSuccess = session.Login(record.Username, record.Password);
                if (!loginSuccess)
                {
                    string res = _loginView.AskRetry();
                    if (res == "2")
                    {
                        session.Logout();
                        Application.Close();
                    }
                }
            } while (!loginSuccess);

            return record;
        }
    }
}
