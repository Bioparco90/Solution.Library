using BusinessLogic.Library.Enums;
using BusinessLogic.Library.Exceptions;
using BusinessLogic.Library.Interfaces;
using Model.Library;

namespace ConsoleApp.Library.Views
{
    internal class AdminView
    {
        private readonly Utils _utils;
        private readonly IBookHandler _bookHandler;
        private readonly IReservationHandler _reservationHandler;

        public AdminView(Utils utils, IBookHandler bookHandler, IReservationHandler reservationHandler)
        {
            _utils = utils;
            _bookHandler = bookHandler;
            _reservationHandler = reservationHandler;
        }

        public void HomeMenu()
        {
            Console.WriteLine("1. Add new book");
            Console.WriteLine("2. Update book");
            Console.WriteLine("3. Delete book");
            Console.WriteLine("4. Search book");
            Console.WriteLine("5. Loan book");
            Console.WriteLine("6. Give back book");
            Console.WriteLine("7. Reservations history");
            Console.WriteLine("8. Exit");
        }

        public void View(Func<bool> action, string successInfo)
        {
            try
            {
                string message = action() ? successInfo : "Something went wrong";
                Console.WriteLine(message);
            }
            catch (BookOnLoanException ex)
            {
                ShowBooksOnLoan(ex.ActiveReservations);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public bool AddBook()
        {
            var book = BuildBook(Method.Add);
            return _bookHandler.Upsert(book);
        }

        public bool UpdateBook()
        {
            var book = BuildBook(Method.Update);
            var found = _bookHandler.SearchSingle(book, parametersCount => parametersCount == 4);
            if (found is null)
            {
                return false;
            }

            var newBook = BuildBook(Method.Update);
            newBook.Id = found.Id;
            newBook.Quantity = found.Quantity;

            return _bookHandler.Update(newBook);
        }

        public bool DeleteBook()
        {
            var book = BuildBook(Method.Delete);
            var found = _bookHandler.SearchSingle(book, parametersCount => parametersCount == 4);
            if (found is null)
            {
                throw new BookSearchException("Book not found");
            }

            var activeReservations = _reservationHandler.GetActiveReservation(found.Id).ToList();
            var canDeleteBook = activeReservations.Count == 0;
            if (!canDeleteBook)
            {
                throw new BookOnLoanException(activeReservations);
            }

            return _bookHandler.Delete(found);
        }

        public IEnumerable<Book> SearchBook()
        {
            var book = BuildBook(Method.Get);

        }

        private Book BuildBook(Method method)
        {
            SearchBooksParams bookParams = new();
            int quantity = 0;

            Console.WriteLine("All the following fields are mandatory");
            switch (method)
            {
                case Method.Update:
                case Method.Delete:
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

        private void ShowBooksOnLoan(IEnumerable<ActiveReservation> actives) => actives.ToList().ForEach(Console.WriteLine);
    }
}
