namespace BusinessLogic.Library.Exceptions
{
    [Serializable]
    public class MandatoryFieldException : Exception
    {
        public MandatoryFieldException()
        {
        }

        public MandatoryFieldException(string? message) : base(message)
        {
        }

        public MandatoryFieldException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}