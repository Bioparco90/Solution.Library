using DataAccessLayer.Library.Interfaces;
using Model.Library;

namespace DataAccessLayer.Library
{
    public class BookHandler : GenericDataHandler<Book>, IBook
    {
        public BookHandler(DataTableAccess<Book> dataAccess) : base(dataAccess)
        {
        }

        public IEnumerable<Book>? GetByProperties(SearchBooksParams searchParams)
        {
            var books = GetAll();

            if(searchParams.Title != null)
            {
                books = books.Where(book => book.Title.ToLower().Contains(searchParams.Title.ToLower()));
            }
            if(searchParams.AuthorName != null)
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


    }
}
