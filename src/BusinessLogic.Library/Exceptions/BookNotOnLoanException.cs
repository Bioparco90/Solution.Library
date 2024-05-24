using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Library.Exceptions
{
    public class BookNotOnLoanException : Exception
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
