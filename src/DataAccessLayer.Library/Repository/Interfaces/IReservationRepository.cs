using Model.Library;

namespace DataAccessLayer.Library.Repository.Interfaces
{
    public interface IReservationRepository
    {
        public IEnumerable<Reservation> GetAll();
        public IEnumerable<HumanReadableReservation> GetAllReadable();
        public IEnumerable<HumanReadableReservation> GetByProperties(Dictionary<string, object> properties);
        public IEnumerable<HumanReadableReservation> GetActives();
        public IEnumerable<HumanReadableReservation> GetActives(Guid bookId);

        public bool Create(Guid userId, Guid bookId);
        public bool Update(Guid id, Dictionary<string, object> parameters);
    }
}