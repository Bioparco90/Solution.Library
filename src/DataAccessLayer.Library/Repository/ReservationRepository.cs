using DataAccessLayer.Library.DAO.Interfaces;
using DataAccessLayer.Library.Repository.Interfaces;
using Model.Library;

namespace DataAccessLayer.Library.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly IReservationDAO _dao;

        public ReservationRepository(IReservationDAO dao)
        {
            _dao = dao;
        }

        public IEnumerable<Reservation> GetAll() => _dao.GetAll();
        public IEnumerable<HumanReadableReservation> GetAllReadable() => _dao.GetAllReadable();
        public IEnumerable<HumanReadableReservation> GetAllReadable(string username) => _dao.GetAllReadable(username);
        public IEnumerable<HumanReadableReservation> GetActives() => _dao.GetActives();
        public IEnumerable<HumanReadableReservation> GetActives(Guid bookId) => _dao.GetActives(bookId);
        public IEnumerable<HumanReadableReservation> GetByProperties(Dictionary<string, object> properties) => _dao.GetByProperties(properties);

        public bool Create(Guid userId, Guid bookId) => _dao.Create(userId, bookId);

        public bool Update(Guid id, Dictionary<string, object> parameters) => _dao.Update(id, parameters);
    }
}
