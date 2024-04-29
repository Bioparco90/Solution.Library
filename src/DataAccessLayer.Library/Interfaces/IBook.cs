using Model.Library;

namespace DataAccessLayer.Library.Interfaces
{
    internal interface IBook
    {
        public bool AddMany(Book item, int quantity);
        public IEnumerable<Book> GetByTitle(string title);
        public IEnumerable<Book> GetByAuthorName(string name);
        public IEnumerable<Book> GetByAuthorSurname(string surname);
        public IEnumerable<Book> GetByPublishingHouse(string publishingHouse);
        public IEnumerable<Book> GetByQuantity(int quantity);

        public IEnumerable<Book> GetByProperties(SearchBooksParams searchParams);
    }
}
