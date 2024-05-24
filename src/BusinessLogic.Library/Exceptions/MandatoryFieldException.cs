namespace BusinessLogic.Library.Exceptions
{
    public class MandatoryFieldException : CustomException
    {
        public MandatoryFieldException()
        {
        }

        public MandatoryFieldException(string message) : base(message)
        {
        }

        public MandatoryFieldException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}