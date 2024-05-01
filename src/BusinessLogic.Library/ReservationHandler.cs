using BusinessLogic.Library.Interfaces;
using DataAccessLayer.Library;
using Model.Library;

namespace BusinessLogic.Library
{
    public class ReservationHandler : GenericDataHandler<Reservation>, IReservation
    {
        public ReservationHandler(DataTableAccess<Reservation> dataAccess) : base(dataAccess)
        {
        }

        public override bool Add(Reservation item)
        {
            var reservation = GetSingleOrNull(item);
            if (reservation != null)
            {
                return false;
            }

            item.Id = Guid.NewGuid();
            return base.Add(item);
        }

        // TODO: Verificare eventualità di valori null in res (nel caso, filtrare)
        public IEnumerable<Reservation> GetByBook(SearchBooksParams book)
        {
            DataTableAccess<Book> da = new();
            BookHandler books = new(da);

            var booksFound = books.GetByProperties(book).ToList() ?? [];

            List<Reservation> res = new();
            booksFound.ForEach(book => res.AddRange(GetByBookId(book.Id)));

            return res;
        }

        public IEnumerable<Reservation> GetByBookId(Guid bookId) => GetAll().Where(r => r.BookId == bookId);

        public IEnumerable<Reservation> GetByStartDate(DateTime start) => GetAll().Where(r => r.StartDate == start);

        public IEnumerable<Reservation> GetByEndDate(DateTime end) => GetAll().Where(r => r.EndDate == end);

        public IEnumerable<Reservation> GetByInterval(DateTime start, DateTime end) => GetAll().Where(r => r.StartDate >= start && r.StartDate <= end);

        public IEnumerable<Reservation>? GetByUser(string username)
        {
            DataTableAccess<User> da = new();
            UserHandler users = new(da);
            var user = users.GetSingleOrNull(new() { Username = username });
            if (user is null)
            {
                return null;
            }
            return GetByUserId(user.Id);
        }

        public IEnumerable<Reservation> GetByUserId(Guid userId) => GetAll().Where(r => r.UserId == userId);
    }
}
