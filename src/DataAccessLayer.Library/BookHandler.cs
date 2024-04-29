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

        // Stesso discorso per la delete, ha senso differenziare l'eliminazione di UNA COPIA di un libro
        // dall'eliminazione della totalità delle copie di uno stessolibro
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

        // Nulla di particolare per quanto riguarda l'update
        // Valutare i vari get senza strafare troppo
    }
}
