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
    }
}
