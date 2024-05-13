using BusinessLogic.Library.Authentication;

namespace ConsoleApp.Library
{
    internal class Menu
    {
        public MenuUtils Utils;
        private LoginView _loginView;

        public Menu(MenuUtils utils, LoginView loginView)
        {
            Utils = utils;
            _loginView = loginView;
        }

        public static bool CheckInputChoice(string input, int numberOfChoices) =>
            !Enumerable.Range(1, numberOfChoices)
                        .Select(x => x.ToString())
                        .Contains(input)
            || input == string.Empty;

        public bool StartMenu()
        {
            Console.WriteLine("Welcome in our library.\n");

            Console.WriteLine("1. Login");
            Console.WriteLine("2. Exit");

            string choice = Utils.GetStrictInteraction("Insert command", input => CheckInputChoice(input, 2));
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
