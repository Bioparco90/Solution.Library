using BusinessLogic.Library.Authentication;
using BusinessLogic.Library.Exceptions;
using BusinessLogic.Library.Interfaces;
using BusinessLogic.Library.Types;
using DataAccessLayer.Library;
using Model.Library;
using Model.Library.Enums;

namespace BusinessLogic.Library
{
    public class BookHandler : GenericDataHandler<Book>, IBook
    {
        public BookHandler(DataTableAccess<Book> dataAccess) : base(dataAccess)
        {
        }

        public IEnumerable<Book> GetByProperties(SearchBooksParams searchParams)
        {
            var books = GetAll();

            if (searchParams.Title != null)
            {
                books = books.Where(book => book.Title.ToLower().Contains(searchParams.Title.ToLower()));
            }
            if (searchParams.AuthorName != null)
            {
                books = books.Where(book => book.AuthorName.ToLower().Contains(searchParams.AuthorName.ToLower()));
            }
            if (searchParams.AuthorSurname != null)
            {
                books = books.Where(book => book.AuthorSurname.ToLower().Contains(searchParams.AuthorSurname.ToLower()));
            }
            if (searchParams.PublishingHouse != null)
            {
                books = books.Where(book => book.PublishingHouse.ToLower().Contains(searchParams.PublishingHouse.ToLower()));
            }

            return books;
        }

        public IEnumerable<Book> GetByTitle(string title) => GetAll().Where(book => book.Title.ToLower().Contains(title.ToLower()));

        public IEnumerable<Book> GetByAuthorName(string name) => GetAll().Where(book => book.AuthorName.ToLower().Contains(name.ToLower()));

        public IEnumerable<Book> GetByAuthorSurname(string surname) => GetAll().Where(book => book.AuthorSurname.ToLower().Contains(surname.ToLower()));

        public IEnumerable<Book> GetByPublishingHouse(string publishingHouse) => GetAll().Where(book => book.PublishingHouse.ToLower().Contains(publishingHouse.ToLower()));

        public IEnumerable<Book> GetByQuantity(int quantity) => GetAll().Where(book => book.Quantity == quantity);

        public bool Add(Book book, int quantity)
        {
            Session session = Session.GetInstance();
            session.CheckAutorizations();

            var found = Get(book).ToList();
            return found.Count switch
            {
                0 => AddBook(book, quantity),
                1 => UpdateExisting(found[0], quantity),
                _ => false,
            };
        }

        public bool UpdateBook(Book oldBook, Book newBook)
        {
            Session session = Session.GetInstance();
            session.CheckAutorizations();

            newBook.Id = oldBook.Id;
            newBook.Quantity = oldBook.Quantity;
            return base.Update(newBook);
        }

        public new BookDeleteResult Delete(Book item)
        {
            Session session = Session.GetInstance();
            session.CheckAutorizations();

            var bookFound = GetSingleOrNull(item);
            if (bookFound is null)
            {
                return new() { StatusCode = ResultStatus.BookNotFound, Message = "Book not found" };
            }

            DataTableAccess<Reservation> dataTableAccess = new();
            ReservationHandler reservationHandler = new(dataTableAccess);
            var reservations = reservationHandler.GetByBookId(bookFound.Id).ToList();
            bool hasActive = reservations.Any(r => r.EndDate > DateTime.Now);

            if (hasActive)
            {
                List<Reservation> actives = reservations.Where(r => r.EndDate > DateTime.Now).ToList();
                return new() { StatusCode = ResultStatus.BookOnLoan, Message = "There is at least one book on loan", Reservations = reservations };
            }

            if (!reservationHandler.DeleteAll(reservations))
            {
                return new() { StatusCode = ResultStatus.Error, Message = "An error occurred during deletion of reservations" };
            }

            try
            {
                base.Delete(bookFound);
                reservationHandler.Save();
                return new() { StatusCode = ResultStatus.Success, Message = "Book Deleted" };
            }
            catch
            {
                return new() { StatusCode = ResultStatus.Error, Message = "Something goes wrong during book deletion operations" };
            }
        }

        private bool AddBook(Book book, int quantity)
        {
            book.Id = Guid.NewGuid();
            book.Quantity = quantity;
            return base.Add(book);
        }

        private bool UpdateExisting(Book book, int quantity)
        {
            book.Quantity += quantity;
            return base.Update(book);
        }
    }
}
