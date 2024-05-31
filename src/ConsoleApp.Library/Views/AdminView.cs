using BusinessLogic.Library.Interfaces;
using ConsoleApp.Library.Views.Interfaces;

namespace ConsoleApp.Library.Views
{
    internal partial class AdminView : View, IAdminView
    {
        private readonly IUserHandler _userHandler;

        public AdminView(Utils utils, IUserHandler userHandler, IBookHandler bookHandler, IReservationHandler reservationHandler)
            : base(utils, reservationHandler, bookHandler)
        {
            _userHandler = userHandler;
        }
    }
}
