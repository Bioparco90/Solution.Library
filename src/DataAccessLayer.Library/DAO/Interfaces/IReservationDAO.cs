

using Model.Library;

namespace DataAccessLayer.Library.DAO.Interfaces
{
    public interface IReservationDAO
    {
        bool Create(Guid userId, Guid bookId);
        bool Update(Guid id, Dictionary<string, object> parameters);

        IEnumerable<Reservation> GetAll();
        IEnumerable<HumanReadableReservation> GetAllReadable();
        IEnumerable<HumanReadableReservation> GetAllReadable(string username);
        IEnumerable<HumanReadableReservation> GetActives();
        IEnumerable<HumanReadableReservation> GetActives(Guid bookId);
        IEnumerable<HumanReadableReservation> GetByProperties(Dictionary<string, object> properties);
    }
}