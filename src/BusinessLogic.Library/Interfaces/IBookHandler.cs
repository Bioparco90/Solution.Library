using Model.Library;

namespace BusinessLogic.Library.Interfaces
{
    public interface IBookHandler
    {
        public bool Upsert(Book book);
        public bool Update(Book book);
        public bool Delete(Book book);
        public bool Loan(Book book);
        public bool GiveBackBook(Book book);


        public Book SearchSingle(Book book, Func<int, bool> constraint);
        public IEnumerable<Book> SearchMany(Book book);
        public IEnumerable<Book> GetAll();
    }
}