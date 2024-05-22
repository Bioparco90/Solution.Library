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
        public IEnumerable<ActiveReservation> GetActiveReservation(Guid bookId)
        {
            return _reservationRepository.GetActives(bookId);
        }
    }

}
