using Model.Library;

namespace BusinessLogic.Library.Interfaces
{
    internal interface IBook
    {
        public bool Upsert(Book item, int quantity);
        public bool UpdateBook(Book oldBook, Book newBook);

        public IEnumerable<Book> GetByTitle(string title);
        public IEnumerable<Book> GetByAuthorName(string name);
        public IEnumerable<Book> GetByAuthorSurname(string surname);
        public IEnumerable<Book> GetByPublishingHouse(string publishingHouse);
        public IEnumerable<Book> GetByQuantity(int quantity);

        public IEnumerable<Book> GetByProperties(SearchBooksParams searchParams);
    }
}
