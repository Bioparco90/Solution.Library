using BusinessLogic.Library.Interfaces;
using DataAccessLayer.Library;
using Model.Library;

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

        public override bool Add(Book book)
        {
            var found = Get(book).ToList();
            return found.Count switch
            {
                0 => AddBook(book, 1),
                1 => UpdateExisting(found[0], 1),
                _ => false,
            };
        }

        public bool AddMany(Book book, int quantity)
        {
            var found = Get(book).ToList();
            return found.Count switch
            {
                0 => AddBook(book, quantity),
                1 => UpdateExisting(found[0], quantity),
                _ => false,
            };
        }

        public override bool Delete(Book item)
        {
            var found = GetSingleOrNull(item);

            if(found is null)
            {
                return false;
            }

            if (found.Quantity > 1)
            {
                return UpdateExisting(found, -1);
            }

            return base.Delete(item);
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
