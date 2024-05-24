using Model.Library;

namespace ConsoleApp.Library.Views
{
    internal partial class AdminView
    {
        public bool ReservationsHistory(out List<HumanReadableReservation> reservations)
        {
            string choice;

            AskFilterMode("book");
            choice = _utils.GetStrictInteraction("Insert command", input => _utils.CheckInputChoice(input, 2));

            // scelta se filtrare per libro
            if (choice == "1")
            {
                throw new NotImplementedException();
            }

            AskFilterMode("user");
            choice = _utils.GetStrictInteraction("Insert command", input => _utils.CheckInputChoice(input, 2));
            if (choice == "1")
            {
                throw new NotImplementedException();
            }

            AskFilterMode("status (active/expired");
            choice = _utils.GetInteraction("Insert command (default: NO): ");

            if (choice == "1")
            {
                throw new NotImplementedException();
            }

            reservations = _reservationHandler.GetAllReadable().ToList();
            return reservations.Count > 0;
        }
    }
}
