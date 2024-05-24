using Model.Library;

namespace ConsoleApp.Library.Views
{
    internal partial class AdminView
    {
        public bool ReservationsHistory(out IEnumerable<HumanReadableReservation> reservations)
        {
            reservations = _reservationHandler.GetAllReadable();

            BookFilterLoop(ref reservations);
            UserFilterLoop(ref reservations);
            StatusFilter(ref reservations);

            return reservations.Any();
        }

        private void BookFilterLoop(ref IEnumerable<HumanReadableReservation> reservations)
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

        public void StatusFilter(ref IEnumerable<HumanReadableReservation> reservations)
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

        public bool AskToContinue(string searchArea)
        {
            AskFilterMode($"Would you like to continue with the {searchArea} search?");
            string choice = _utils.GetStrictInteraction("Insert command", input => _utils.CheckInputChoice(input, 2));
            return choice == "1";
        }
    }
}
