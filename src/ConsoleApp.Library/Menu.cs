using BusinessLogic.Library.Authentication;
using ConsoleApp.Library.Views;
using ConsoleApp.Library.Views.Interfaces;
using Model.Library;

namespace ConsoleApp.Library
{
    internal class Menu
    {
        private readonly Session _session;
        private readonly Utils _utils;
        private readonly ILoginView _loginView;
        private readonly AdminView _adminView;
        private readonly UserView _userView;

        public Menu(Session session, Utils utils, ILoginView loginView, AdminView adminView, UserView userView)
        {
            _session = session;
            _utils = utils;
            _loginView = loginView;
            _adminView = adminView;
            _userView = userView;
        }

        public bool StartMenu()
        {
            Console.WriteLine("Welcome in our library.\n");

            Console.WriteLine("1. Login");
            Console.WriteLine("2. Exit");

            string choice = _utils.GetStrictInteraction("Insert command", input => _utils.CheckInputChoice(input, 2));
            return choice == "1";
        }

        public void LoginMenu()
        {
            LoginRecord record;
            bool loginSuccess;
            do
            {
                record = _loginView.AskLogin();
                loginSuccess = _session.Login(record.Username, record.Password);
                if (!loginSuccess)
                {
                    string res = _loginView.AskRetry();
                    if (res == "2")
                    {
                        _session.Logout();
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
                        _adminView.Show(_adminView.AddBook, "Insertion successful");
                        break;

                    case "2":
                        _adminView.Show(_adminView.UpdateBook, "Update successful");
                        break;

                    case "3":
                        _adminView.Show(_adminView.DeleteBook, "Deleted successful");
                        break;

                    case "4":
                        List<Book> books;
                        if (_adminView.SearchBooks(out books))
                        {
                            _adminView.ShowBooks(books);
                        }
                        else
                        {
                            Console.WriteLine("No book meets the search parameters");
                        }
                        break;

                    case "5":
                        _adminView.Show(_adminView.LoanBook, "Loan successful");
                        break;

                    case "6":
                        _adminView.Show(_adminView.GiveBackBook, "Book returned");
                        break;

                    case "7":
                        IEnumerable<HumanReadableReservation> reservations;
                        if (_adminView.ReservationsHistory(out reservations))
                        {
                            _adminView.ShowReservations(reservations);
                        }
                        else
                        {
                            Console.WriteLine("Reservations not found");
                        }
                        break;

                    case "8":
                        Application.Close();
                        break;
                }
            }
        }

        public void UserMenu()
        {
            Console.Clear();
            Console.WriteLine($"Welcome, {_session.LoggedUser}");

            while (true)
            {
                _userView.HomeMenu();
                string choice = _utils.GetStrictInteraction("Insert command", input => _utils.CheckInputChoice(input, 5));

                switch (choice)
                {
                    case "1":
                        List<Book> books;
                        if (_userView.SearchBooks(out books))
                        {
                            _userView.ShowBooks(books);
                        }
                        else
                        {
                            Console.WriteLine("No book meets the search parameters");
                        }
                        break;

                    case "2":
                        _userView.Show(_userView.LoanBook, "Loan successful");
                        break;

                    case "3":
                        _userView.Show(_userView.GiveBackBook, "Book returned");
                        break;

                    case "4":
                        IEnumerable<HumanReadableReservation> reservations;
                        if (_userView.ReservationsHistory(out reservations))
                        {
                            _userView.ShowReservations(reservations);
                        }
                        else
                        {
                            Console.WriteLine("Reservations not found");
                        }
                        break;
                }
            }
        }
    }
}
