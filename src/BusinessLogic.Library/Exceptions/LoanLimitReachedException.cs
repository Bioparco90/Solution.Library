namespace BusinessLogic.Library.Exceptions
{
    public class LoanLimitReachedException : Exception
    {
        public LoanLimitReachedException()
        {
        }

        public LoanLimitReachedException(string? message) : base(message)
        {
        }

        public LoanLimitReachedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
