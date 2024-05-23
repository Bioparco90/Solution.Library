using BusinessLogic.Library.Authentication;
using ConsoleApp.Library.Views;

namespace ConsoleApp.Library
{
    internal class Menu
    {
        private readonly Utils _utils;
        private readonly LoginView _loginView;
        private readonly AdminView _adminView;

        public Menu(Utils utils, LoginView loginView, AdminView adminView)
        {
            _utils = utils;
            _loginView = loginView;
            _adminView = adminView;
        }

        public bool StartMenu()
        {
            Console.WriteLine("Welcome in our library.\n");

            Console.WriteLine("1. Login");
            Console.WriteLine("2. Exit");

            string choice = _utils.GetStrictInteraction("Insert command", input => _utils.CheckInputChoice(input, 2));
            return choice == "1";
        }

        public void LoginMenu(Session session)
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
        }

        public void AdminMenu()
        {
            Console.Clear();
            Console.WriteLine($"Welcome, Administrator");

            while (true)
            {
                _adminView.HomeMenu();
                string choice = _utils.GetStrictInteraction("Insert command", input => _utils.CheckInputChoice(input, 8));

                switch (choice)
                {
                    case "1":
                        _adminView.View(_adminView.AddBook, "Insertion successfull");                
                        break;

                    case "2":
                        _adminView.View(_adminView.UpdateBook, "Update successfull");
                        break;

                    case "3":
                        _adminView.View(_adminView.DeleteBook, "Deleted successfull");
                        break;

                    case "4":
                        _adminView.SearchBook();
                        break;

                    case "5":
                        throw new NotImplementedException();
                        break;

                    case "6":
                        throw new NotImplementedException();
                        break;

                    case "7":
                        throw new NotImplementedException();
                        break;

                    case "8":
                        Application.Close();
                        break;

                    default:
                        break;

                }
            }
        }
    }
}
