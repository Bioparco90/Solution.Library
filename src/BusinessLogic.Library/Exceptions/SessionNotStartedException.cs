namespace BusinessLogic.Library.Exceptions
{
    public class SessionNotStartedException : CustomException
    {
        public SessionNotStartedException(string message) : base(message) { }
    }
}
