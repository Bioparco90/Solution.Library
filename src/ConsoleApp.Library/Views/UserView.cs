using BusinessLogic.Library.Interfaces;

namespace ConsoleApp.Library.Views
{
    internal class UserView : View
    {
        public UserView(Utils utils, IReservationHandler reservationHandler, IBookHandler bookHandler)
            : base(utils, reservationHandler, bookHandler)
        {
        }

        public override void HomeMenu()
        {
            Console.WriteLine("1. Search book");
            Console.WriteLine("2. Loan book");
            Console.WriteLine("3. Give back book");
            Console.WriteLine("4. Reservations history");
            Console.WriteLine("5. Exit");
        }
    }
}
