using BusinessLogic.Library.Authentication;
using BusinessLogic.Library.Interfaces;
using DataAccessLayer.Library.Repository.Interfaces;
using Model.Library;

namespace BusinessLogic.Library
{
    public class ReservationHandler : IReservationHandler
    {
        private readonly Session _session;
        private readonly IReservationRepository _reservationRepository;

        public ReservationHandler(Session session, IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
            _session = session;
        }

        public IEnumerable<HumanReadableReservation> GetAllReadable() => _reservationRepository.GetAllReadable();
        public IEnumerable<HumanReadableReservation> GetAllReadable(string username) => _reservationRepository.GetAllReadable(username);

        public IEnumerable<HumanReadableReservation> GetActiveReservation(Guid bookId) => _reservationRepository.GetActives(bookId);

        public bool CreateReservation(Guid bookId) => _reservationRepository.Create(_session.UserId, bookId);

        public bool CloseReservation(Guid id)
        {
            Dictionary<string, object> parameters = new()
            {
                {"EndDate", DateTime.Now}
            };
            return _reservationRepository.Update(id, parameters);
        }

    }

}
