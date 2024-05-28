using BusinessLogic.Library.Authentication;
using BusinessLogic.Library.Interfaces;
using ConsoleApp.Library.Views.Interfaces;

namespace ConsoleApp.Library.Views
{
    internal partial class AdminView : View, IAdminView
    {
        private readonly IUserHandler _userHandler;
        private Session _session;

        public AdminView(Session session, Utils utils, IUserHandler userHandler, IBookHandler bookHandler, IReservationHandler reservationHandler)
            : base(utils, reservationHandler, bookHandler)
        {
            _session = session;
            _userHandler = userHandler;
        }
    }
}
