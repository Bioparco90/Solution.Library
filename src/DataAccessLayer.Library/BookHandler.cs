using Model.Library;

namespace DataAccessLayer.Library
{
    public class BookHandler : GenericDataHandler<Book>
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

            book.Id = Guid.NewGuid();
            book.Quantity = 1;
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
