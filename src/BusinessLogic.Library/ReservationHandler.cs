using BusinessLogic.Library.Interfaces;
using DataAccessLayer.Library;
using Model.Library;
using System.Net;

namespace BusinessLogic.Library
{
    public class ReservationHandler : GenericDataHandler<Reservation>, IReservation
    {
        public ReservationHandler(DataTableAccess<Reservation> dataAccess) : base(dataAccess)
        {
        }

        public override bool Add(Reservation item)
        {
            Reservation reservation = Get(item);
            if(reservation != null)
            {
                return false;
            }

            item.Id = Guid.NewGuid();
            return base.Add(item);
        }

        public IEnumerable<Reservation> GetByBook(Book book)
        {
            DataTableAccess<Book> da = new();
            BookHandler books = new(da);

            var bookFound = books.Get(book);
            return GetByBookId(bookFound.Id);
        }

        public IEnumerable<Reservation> GetByBookId(Guid bookId) => GetAll().Where(r => r.BookId == bookId);

        public IEnumerable<Reservation> GetByStartDate(DateTime start) => GetAll().Where(r => r.StartDate == start);

        public IEnumerable<Reservation> GetByEndDate(DateTime end) => GetAll().Where(r => r.EndDate == end);

        public IEnumerable<Reservation> GetByInterval(DateTime start, DateTime end) => GetAll().Where(r => r.StartDate >= start && r.StartDate <= end);

        public IEnumerable<Reservation> GetByUser(string username)
        {
            DataTableAccess<User> da = new();
            UserHandler users = new(da);
            var user = users.Get(new() { Username = username });
            return GetByUserId(user.Id);
        }

        public IEnumerable<Reservation> GetByUserId(Guid userId) => GetAll().Where(r => r.UserId == userId);
    }
}
