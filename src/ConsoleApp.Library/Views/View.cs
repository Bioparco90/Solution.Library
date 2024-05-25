using BusinessLogic.Library.Enums;
using BusinessLogic.Library.Interfaces;
using Model.Library;

namespace ConsoleApp.Library.Views
{
    internal abstract partial  class View
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

        public virtual bool ReservationsHistory(out IEnumerable<HumanReadableReservation> reservations)
        {
            reservations = _reservationHandler.GetAllReadable();

            BookFilterLoop(ref reservations);
            StatusFilter(ref reservations);

            return reservations.Any();
        }
    }
}
