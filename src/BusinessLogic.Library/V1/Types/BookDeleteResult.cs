using Model.Library;

namespace BusinessLogic.Library.V1.Types
{
    public class BookDeleteResult : Result
    {
        public List<Reservation> Reservations { get; set; } = new();
    }
}
