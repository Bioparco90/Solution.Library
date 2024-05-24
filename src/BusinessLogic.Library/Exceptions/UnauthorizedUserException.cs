namespace BusinessLogic.Library.Exceptions
{
    public class UnauthorizedUserException : CustomException
    {
        public UnauthorizedUserException(string message) : base(message)
        {
        }
    }
}
