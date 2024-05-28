using Model.Library;

namespace ConsoleApp.Library.Views.Interfaces
{
    internal interface IView
    {
        bool GiveBackBook();
        void HomeMenu();
        bool LoanBook();
        bool ReservationsHistory(out IEnumerable<HumanReadableReservation> reservations);
        bool SearchBooks(out List<Book> books);
        void Show(Func<bool> action, string successInfo);
        void ShowBooks(IEnumerable<Book> books);
        void ShowReservations(IEnumerable<HumanReadableReservation> reservations);
    }
}