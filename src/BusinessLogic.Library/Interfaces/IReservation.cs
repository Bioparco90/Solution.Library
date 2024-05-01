using Model.Library;

namespace BusinessLogic.Library.Interfaces
{
    internal interface IReservation
    {
        public IEnumerable<Reservation> GetByUserId(Guid userId);
        public IEnumerable<Reservation>? GetByUser(string username);

        public IEnumerable<Reservation> GetByBookId(Guid bookId);
        public IEnumerable<Reservation> GetByBook(SearchBooksParams book);

        public IEnumerable<Reservation> GetByStartDate(DateTime start);
        public IEnumerable<Reservation> GetByEndDate(DateTime end);
        public IEnumerable<Reservation> GetByInterval(DateTime start, DateTime end);

    }
}
