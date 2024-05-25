using Model.Library;

namespace BusinessLogic.Library.Interfaces
{
    public interface IReservationHandler
    {
        public IEnumerable<HumanReadableReservation> GetAllReadable();
        public IEnumerable<HumanReadableReservation> GetActiveReservation(Guid bookId);
        public bool CreateReservation(Guid bookId);
        public bool CloseReservation(Guid id);
    }
}