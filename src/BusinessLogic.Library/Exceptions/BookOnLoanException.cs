using Model.Library;

namespace BusinessLogic.Library.Exceptions
{
    public class BookOnLoanException : CustomException
    {
        public IEnumerable<HumanReadableReservation>? ActiveReservations;

        public BookOnLoanException(IEnumerable<HumanReadableReservation> actives)
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
