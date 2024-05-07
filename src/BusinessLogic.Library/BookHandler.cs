using BusinessLogic.Library.Interfaces;
using DataAccessLayer.Library;
using Model.Library;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            var found = Get(book).ToList();
            return found.Count switch
            {
                0 => AddBook(book, quantity),
                1 => UpdateExisting(found[0], quantity),
                _ => false,
            };
        }

        // TODO: Non deve essere possibile modificare la quantità del libro, ma solamente i dati Anagrafici
        // URGENT: Verificare la logica del metodo.
        // Potrebbe avere senso demandare la creazione del new book direttamente alla View (id compreso)
        public bool UpdateBook(Guid bookId, Book newBook)
        {
            var book = Get(newBook).ToList();
            if(book.Count != 0)
            {
                return false;
            }

            newBook.Id = bookId;
            return base.Update(newBook);
        }

        // URGENT: sistemare il tipo di ritorno
        // in modo da permettere la restituzione di una eventuale lista di reservation insieme al booleano
        // proabilmente si può utilizzare la classe ReservationResult con le dovute modifiche
        public override bool Delete(Book item)
        {
            var bookFound = GetSingleOrNull(item);
            if (bookFound is null)
            {
                return false;
            }

            DataTableAccess<Reservation> dataTableAccess = new();
            ReservationHandler reservationHandler = new(dataTableAccess);
            var reservations = reservationHandler.GetByBookId(bookFound.Id).ToList();
            bool hasActive = reservations.Any(r => r.EndDate > DateTime.Now);

            if (hasActive)
            {
                return false;
            }

            if (!reservationHandler.DeleteAll(reservations))
            {
                return false;
            }
            reservationHandler.Save();

            return base.Delete(bookFound);
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
