using BusinessLogic.Library.Authentication;
using BusinessLogic.Library.Interfaces;

namespace ConsoleApp.Library.Views
{
    internal partial class AdminView
    {
        private readonly Utils _utils;
        private readonly IBookHandler _bookHandler;
        private readonly IReservationHandler _reservationHandler;
        private Session _session;

        public AdminView(Session session, Utils utils, IBookHandler bookHandler, IReservationHandler reservationHandler)
        {
            _session = session;
            _utils = utils;
            _bookHandler = bookHandler;
            _reservationHandler = reservationHandler;
        }
    }
}
