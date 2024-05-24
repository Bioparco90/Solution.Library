using Model.Library;

namespace BusinessLogic.Library.Exceptions
{
    public class BookOnLoanException : CustomException
    {
        public IEnumerable<ActiveReservation>? ActiveReservations;

        public BookOnLoanException(IEnumerable<ActiveReservation> actives)
        {
            ActiveReservations = actives;
        }

        public BookOnLoanException(string message) : base(message)
        {
        }

        public BookOnLoanException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
