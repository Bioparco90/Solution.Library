using DataAccessLayer.Library;
using Model.Library;

namespace BusinessLogic.Library
{
    public interface ILibraryBusinessLogic
    {
        public void Login(User user);
        public void AddBook(Book book);
        public Result UpdateBook(Book book);
        public Result DeleteBook(Book book);
        public List<Book>? GetBooks(SearchBooksParams parameters);
        public Book? BorrowBook(Book book);
        public void GiveBackBook(Book book);
        public List<ReservationResult> History(SearchHistoryParams parameters);
        public void Exit();
    }
}
