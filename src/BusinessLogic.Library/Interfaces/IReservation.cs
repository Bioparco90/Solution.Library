using Model.Library;

namespace BusinessLogic.Library.Interfaces
{
    internal interface IReservation
    {
        public bool Create(User user, Book book);
        public bool EndReservation(User user, Book book);
        public bool DeleteAll(IEnumerable<Reservation> listToDelete);

        public IEnumerable<Reservation> GetByUserId(Guid userId);
        public IEnumerable<Reservation>? GetByUser(string username);

        public IEnumerable<Reservation> GetByBookId(Guid bookId);
        public IEnumerable<Reservation> GetByBook(SearchBooksParams book);

        public IEnumerable<Reservation> GetByStartDate(DateTime start);
        public IEnumerable<Reservation> GetByEndDate(DateTime end);
        public IEnumerable<Reservation> GetByInterval(DateTime start, DateTime end);

        public IEnumerable<Reservation> GetByState(bool isActive);
        public IEnumerable<Reservation> GetByStateAndUser(bool isActive, Guid userId);

        public bool IsAvailable(Book book);
        public IEnumerable<Reservation> CheckUserActiveReservations(User user, Book foundBook);
    }
}
