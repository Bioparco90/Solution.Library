using BusinessLogic.Library.Enums;
using Model.Library;

namespace ConsoleApp.Library.Views
{
    internal partial class AdminView
    {
        private void AskFilterMode(string filter)
        {
            Console.WriteLine($"Do you want to filter by {filter}?");
            Console.WriteLine("1. Yes");
            Console.WriteLine("2. No");
        }

        private Book BuildBook(Method method)
        {
            SearchBooksParams bookParams = new();
            int quantity = 0;

            switch (method)
            {
                case Method.Delete:
                case Method.Update:
                case Method.EndLoan:
                case Method.Loan:
                    bookParams = AskStrictAnagraphic();
                    break;

                case Method.Get:
                    bookParams = AskAnagraphic();
                    break;

                case Method.Add:
                    bookParams = AskStrictAnagraphic();
                    quantity = _utils.GetStrictInteraction("Quantity");
                    break;

                default:
                    break;
            }

            return new()
            {
                Title = bookParams.Title,
                AuthorName = bookParams.AuthorName,
                AuthorSurname = bookParams.AuthorSurname,
                PublishingHouse = bookParams.PublishingHouse,
                Quantity = quantity
            };
        }

        private delegate string InteractionDelegate(string message);
        private SearchBooksParams AskAnagraphicCommon(InteractionDelegate interaction)
        {
            string title = interaction("Title");
            string authorName = interaction("Author Name");
            string authorSurname = interaction("Author Surname");
            string publishingHouse = interaction("Publishing House");

            return new()
            {
                Title = title,
                AuthorName = authorName,
                AuthorSurname = authorSurname,
                PublishingHouse = publishingHouse,
            };
        }

        private SearchBooksParams AskAnagraphic() => AskAnagraphicCommon(_utils.GetInteraction);
        private SearchBooksParams AskStrictAnagraphic() => AskAnagraphicCommon(message => _utils.GetStrictInteraction(message, _utils.CheckEmpty));

        public void ShowReservations(IEnumerable<HumanReadableReservation> reservations)
        {
            int index = 0;
            reservations.ToList().ForEach(r =>
            {
                index++;
                Console.WriteLine($"{index}) {r}");
            });
        }

        private void ShowBooksOnLoan(IEnumerable<HumanReadableReservation> actives)
        {
            actives.ToList().ForEach(r =>
            {
                Console.WriteLine($"The cancellation was not carried out because the book {r.Title} is still reserved by the user {r.Username} from {r.StartDate:dd/MM/yyyy} to {r.EndDate:dd/MM/yyyy}.");
            });
        }

        public void ShowBooks(IEnumerable<Book> books)
        {
            foreach (Book book in books)
            {
                Console.WriteLine(book);
                var actives = _reservationHandler.GetActiveReservation(book.Id).ToList();
                if (actives.Count < book.Quantity)
                {
                    Console.WriteLine($"{book.Title} is currently available for loan!");
                }
                else
                {
                    var nextAvailable = actives.OrderByDescending(a => a.EndDate).First();
                    Console.WriteLine($"{book.Title} is currently on loan");
                    Console.WriteLine($"The book can be borrowed starting from {nextAvailable.EndDate.AddDays(1)}");
                }
            }
        }
    }
}
