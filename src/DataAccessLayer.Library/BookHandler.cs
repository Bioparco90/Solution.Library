using DataAccessLayer.Library.Interfaces;
using Model.Library;

namespace DataAccessLayer.Library
{
    public class BookHandler : GenericDataHandler<Book>, IBook
    {
        public BookHandler(DataTableAccess<Book> dataAccess) : base(dataAccess)
        {
        }
        public override bool Add(Book book)
        {
            Book? found = Get(book);

            if (found != null)
            {
                found.Quantity++;
                Update(found);
                return true;
            }

            return AddBook(book, 1);
        }

        public bool AddMany(Book book, int quantity)
        {
            Book? found = Get(book);
            if (found != null)
            {
                found.Quantity += quantity;
                Update(found);
                return true;
            }

            return AddBook(book, quantity);
        }

        private bool AddBook(Book book, int quantity)
        {
            book.Id = Guid.NewGuid();
            book.Quantity = quantity;
            return base.Add(book);
        }

        public override bool Delete(Book item)
        {
            var found = Get(item);
            if (found != null && found?.Quantity > 1)
            {
                found.Quantity--;
                Update(found);
                return true;
            }

            return base.Delete(item);
        }

        // TODO: fare i vari get per la ricerca di un libro
    }
}
