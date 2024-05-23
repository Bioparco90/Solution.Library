using Model.Library;

namespace BusinessLogic.Library.Interfaces
{
    public interface IReservationHandler
    {
        public IEnumerable<ActiveReservation> GetActiveReservation(Guid bookId);
        public bool CreateReservation(Guid bookId);
    }
}