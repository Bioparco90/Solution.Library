using BusinessLogic.Library.Enums;
using BusinessLogic.Library.Interfaces;
using ConsoleApp.Library.Views.Interfaces;
using Model.Library;

namespace ConsoleApp.Library.Views
{
    internal abstract partial class View : IView
    {
        protected readonly Utils _utils;
        protected readonly IReservationHandler _reservationHandler;
        protected readonly IBookHandler _bookHandler;

        public View(Utils utils, IReservationHandler reservationHandler, IBookHandler bookHandler)
        {
            _utils = utils;
            _reservationHandler = reservationHandler;
            _bookHandler = bookHandler;
        }

        public abstract void HomeMenu();
        public abstract bool ReservationsHistory(out IEnumerable<HumanReadableReservation> reservations);

        public bool SearchBooks(out List<Book> books)
        {
            var book = BuildBook(Method.Get);
            books = _bookHandler.SearchMany(book).ToList();
            return books.Count > 0;
        }

        public bool LoanBook()
        {
            Console.WriteLine("All the following fields are mandatory");

            var book = BuildBook(Method.Loan);
            return _bookHandler.Loan(book);
        }

        public bool GiveBackBook()
        {
            var book = BuildBook(Method.EndLoan);
            return _bookHandler.GiveBackBook(book);
        }

        protected void BookFilterLoop(ref IEnumerable<HumanReadableReservation> reservations)
        {
            AskFilterMode("Would you like to search by book?");
            string choice = _utils.GetStrictInteraction("Insert command", input => _utils.CheckInputChoice(input, 2));

            if (choice == "1")
            {
                bool endLoop = false;
                do
                {
                    var book = BuildBook(BusinessLogic.Library.Enums.Method.Get);
                    try
                    {
                        var found = _bookHandler.SearchSingle(book, parametersCount => parametersCount == 4);
                        reservations = reservations.Where(r => r.Title == found.Title);
                        endLoop = true;
                    }
                    catch (InvalidOperationException)
                    {
                        Console.WriteLine("The search is ambiguous, please be more specific.");
                        endLoop = !AskToContinue("book");
                    }
                }
                while (!endLoop);
            }
        }

        protected void StatusFilter(ref IEnumerable<HumanReadableReservation> reservations)
        {
            AskFilterMode("Would you like to search by status (active/expired)?");
            string choice = _utils.GetInteraction("Insert command (default: NO): ");

            if (choice == "1")
            {
                Console.WriteLine("1. Active");
                Console.WriteLine("2. Expired");
                choice = _utils.GetStrictInteraction("Insert command", input => _utils.CheckInputChoice(input, 2));
                string status = choice == "1" ? "Active" : "Expired";
                reservations = reservations.Where(r => r.Status == status);
            }
        }
    }
}
