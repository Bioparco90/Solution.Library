﻿using BusinessLogic.Library.Authentication;
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
            catch (BookOnLoanException ex) when (ex.ActiveReservations is not null)
            {
                ShowBooksOnLoan(ex.ActiveReservations!);
            }
            catch (CustomException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("An error occurred in database:\n" + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected Error occurred:\n" + ex.Message);
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
            var found = _bookHandler.SearchSingle(book, parametersCount => parametersCount == 4);

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

        public bool SearchBooks(out List<Book> books)
        {
            var book = BuildBook(Method.Get);
            books = _bookHandler.SearchMany(book).ToList();
            return books.Count > 0;
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
            return _bookHandler.GiveBackBook(book);
        }

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
