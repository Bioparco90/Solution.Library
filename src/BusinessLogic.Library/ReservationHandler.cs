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

        public IEnumerable<Reservation> GetByStartDate(DateTime start)
        {
            // TODO: implement ReservationHandler GetByStartDate method
            throw new NotImplementedException();
        }

        public IEnumerable<Reservation> GetByEndDate(DateTime end)
        {
            // TODO: implement ReservationHandler GetByEndDate method
            throw new NotImplementedException();
        }

        public IEnumerable<Reservation> GetByInterval(DateTime start, DateTime end)
        {            
            // TODO: implement ReservationHandler GetByInterval method
            throw new NotImplementedException();
        }

        public IEnumerable<Reservation> GetByUser(string username)
        {
            // TODO: implement ReservationHandler GetByUser method
            throw new NotImplementedException();
        }

        public IEnumerable<Reservation> GetByUserId(Guid userId)
        {
            // TODO: implement ReservationHandler GetByUserId method
            throw new NotImplementedException();
        }
    }
}
