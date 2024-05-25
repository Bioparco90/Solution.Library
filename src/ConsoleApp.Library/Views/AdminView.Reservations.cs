using Model.Library;

namespace ConsoleApp.Library.Views
{
    internal partial class AdminView
    {
        public override bool ReservationsHistory(out IEnumerable<HumanReadableReservation> reservations)
        {
            reservations = _reservationHandler.GetAllReadable();

            BookFilterLoop(ref reservations);
            UserFilterLoop(ref reservations);
            StatusFilter(ref reservations);

            return reservations.Any();
        }

        private void UserFilterLoop(ref IEnumerable<HumanReadableReservation> reservations)
        {
            AskFilterMode("Would you like to search by user?");
            string choice = _utils.GetStrictInteraction("Insert command", input => _utils.CheckInputChoice(input, 2));
            if (choice == "1")
            {
                bool endLoop = false;
                do
                {
                    string username = _utils.GetStrictInteraction("Username", _utils.CheckEmpty);
                    if (_userHandler.CheckUser(username))
                    {
                        reservations = reservations.Where(_r => _r.Username == username);
                        endLoop = true;
                    }
                    else
                    {
                        Console.WriteLine("User doesn't exists");
                        endLoop = !AskToContinue("user");
                    }
                } while (!endLoop);
            }
        }
    }
}
