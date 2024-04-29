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

        public IEnumerable<Reservation> GetByBook(Book book)
        {
            DataTableAccess<Book> da = new();
            BookHandler books = new(da);

            var bookFound = books.Get(book);
            return GetByBookId(bookFound.Id);
            
        }

        public IEnumerable<Reservation> GetByBookId(Guid bookId)
        {
            return GetAll().Where(r => r.BookId == bookId);
        }

        public IEnumerable<Reservation> GetByEndDate(DateTime end)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Reservation> GetByInterval(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Reservation> GetByStartDate(DateTime start)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Reservation> GetByUser(string username)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Reservation> GetByUserId(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
