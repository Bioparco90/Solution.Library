using BusinessLogic.Library.Authentication;
using BusinessLogic.Library.Interfaces;
using Model.Library;

namespace ConsoleApp.Library.Views
{
    internal class UserView : View
    {
        private readonly Session _session;
        public UserView(Session session, Utils utils, IReservationHandler reservationHandler, IBookHandler bookHandler)
            : base(utils, reservationHandler, bookHandler)
        {
            _session = session;
        }

        public override void HomeMenu()
        {
            Console.WriteLine("1. Search book");
            Console.WriteLine("2. Loan book");
            Console.WriteLine("3. Give back book");
            Console.WriteLine("4. Reservations history");
            Console.WriteLine("5. Exit");
        }

        public override bool ReservationsHistory(out IEnumerable<HumanReadableReservation> reservations)
        {
            reservations = _reservationHandler.GetAllReadable(_session.LoggedUser);
            BookFilterLoop(ref reservations);
            StatusFilter(ref reservations);

            return reservations.Any();
        }

        protected override void BookFilterLoop(ref IEnumerable<HumanReadableReservation> reservations)
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

        protected override void StatusFilter(ref IEnumerable<HumanReadableReservation> reservations)
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
