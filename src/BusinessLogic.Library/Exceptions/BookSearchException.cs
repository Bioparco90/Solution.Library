namespace BusinessLogic.Library.Exceptions
{
    public class BookSearchException : Exception
    {
        public BookSearchException()
        {
        }

        public BookSearchException(string? message) : base(message)
        {
        }

        public BookSearchException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}