using Model.Library;

namespace DataAccessLayer.Library.Repository.Interfaces
{
    public interface IReservationRepository
    {
        public IEnumerable<Reservation> GetAll();
        public bool Create(Guid userId, Guid bookId);
    }
}