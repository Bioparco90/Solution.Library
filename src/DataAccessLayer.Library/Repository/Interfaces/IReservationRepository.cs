using Model.Library;

namespace DataAccessLayer.Library.Repository.Interfaces
{
    public interface IReservationRepository
    {
        public IEnumerable<Reservation> GetAll();
        public IEnumerable<ActiveReservation> GetActives();
        public IEnumerable<ActiveReservation> GetActives(Guid bookId);

        public bool Create(Guid userId, Guid bookId);
        public bool Update(Guid id, Dictionary<string, object> parameters);
    }
}