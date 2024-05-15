namespace BusinessLogic.Library.Exceptions
{
    public class SessionNotStartedException : Exception
    {
        public SessionNotStartedException(string message) : base(message) { }
    }
}
