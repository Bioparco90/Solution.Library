using BusinessLogic.Library.Authentication;
using BusinessLogic.Library.Enums;
using BusinessLogic.Library.Exceptions;
using BusinessLogic.Library.Interfaces;
using Model.Library;
using System.Data.SqlClient;

namespace ConsoleApp.Library.Views
{
    internal class AdminView
    {
        private readonly Utils _utils;
        private readonly IBookHandler _bookHandler;
        private readonly IReservationHandler _reservationHandler;
        private Session _session;

        public AdminView(Session session, Utils utils, IBookHandler bookHandler, IReservationHandler reservationHandler)
        {
            _session = session;
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
            catch (BookOnLoanException ex) when (ex.ActiveReservations is null)
            {
                Console.WriteLine(ex.Message);
            }
            catch (BookOnLoanException ex)
            {
                ShowBooksOnLoan(ex.ActiveReservations!);
            }
            catch (LoanLimitReachedException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public bool AddBook()
        {
            Console.WriteLine("All the following fields are mandatory");
            var book = BuildBook(Method.Add);
            return _bookHandler.Upsert(book);
        }

        public bool UpdateBook()
        {
            Console.WriteLine("All the following fields are mandatory");
            var book = BuildBook(Method.Update);
            var found = _bookHandler.SearchSingle(book, parametersCount => parametersCount == 4)
                ?? throw new BookSearchException("Book not found");

            Console.WriteLine("All the following fields are mandatory");
            var newBook = BuildBook(Method.Update);
            newBook.Id = found.Id;
            newBook.Quantity = found.Quantity;

            return _bookHandler.Update(newBook);
        }

        public bool DeleteBook()
        {
            Console.WriteLine("All the following fields are mandatory");
            var book = BuildBook(Method.Delete);
            return _bookHandler.Delete(book);
        }

        public void SearchBook()
        {
            var book = BuildBook(Method.Get);
            var books = _bookHandler.SearchMany(book).ToList();
            if (books.Count == 0)
            {
                Console.WriteLine("No book meets the search parameters");
                return;
            }
            ShowBooks(books);
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
            throw new NotImplementedException();
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

        private void ShowBooksOnLoan(IEnumerable<ActiveReservation> actives) => actives.ToList().ForEach(Console.WriteLine);
        private void ShowBooks(IEnumerable<Book> books)
        {
            foreach (Book book in books)
            {
                Console.WriteLine(book);
                var actives = _reservationHandler.GetActiveReservation(book.Id).ToList();
                if (actives.Count == 0)
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
