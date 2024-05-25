using Model.Library;

namespace BusinessLogic.Library.Types
{
    public class BookDeleteResult : Result
    {
        public List<Reservation> Reservations { get; set; } = new();
    }
}
