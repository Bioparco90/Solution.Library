using Model.Library.Enums;

namespace BusinessLogic.Library.Types
{
    public class Result
    {
        public ResultStatus StatusCode { get; set; }
        public string? Message { get; set; }
    }
}
