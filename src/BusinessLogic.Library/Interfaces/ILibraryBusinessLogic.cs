using Model.Library;

namespace BusinessLogic.Library.Interfaces
{

    // TODO: valutare utilità di questa interfaccia finora inutilizzata
    public interface ILibraryBusinessLogic
    {
        public void Login(User user);
        public void AddBook(Book book);
        public LoginResult UpdateBook(Book book);
        public LoginResult DeleteBook(Book book);
        public List<Book>? GetBooks(SearchBooksParams parameters);
        public Book? BorrowBook(Book book);
        public void GiveBackBook(Book book);
        public List<ReservationResult> History(SearchHistoryParams parameters);
        public void Exit();
    }
}
