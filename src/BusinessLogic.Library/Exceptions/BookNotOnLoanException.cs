namespace BusinessLogic.Library.Exceptions
{
    public class BookNotOnLoanException : CustomException
    {
        public BookNotOnLoanException()
        {
        }

        public BookNotOnLoanException(string message) : base(message)
        {
        }

        public BookNotOnLoanException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
