namespace BusinessLogic.Library.V1.Exceptions
{
    public class SessionNotStartedException : Exception
    {
        public SessionNotStartedException(string message) : base(message) { }
    }
}
