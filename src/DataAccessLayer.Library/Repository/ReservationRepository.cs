using DataAccessLayer.Library.DAO;
using DataAccessLayer.Library.Repository.Interfaces;
using Model.Library;

namespace DataAccessLayer.Library.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ReservationDAO _dao;

        public ReservationRepository(ReservationDAO dao)
        {
            _dao = dao;
        }

        public IEnumerable<Reservation> GetAll() => _dao.GetAll();

        public bool Create(Guid userId, Guid bookId) => _dao.Create(userId, bookId);

        public bool Update(Guid id, Dictionary<string, object> parameters) => _dao.Update(id, parameters);

    }
}
