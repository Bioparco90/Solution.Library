using Model.Library.Enums;

namespace BusinessLogic.Library.V1.Types
{
    public class Result
    {
        public ResultStatus StatusCode { get; set; }
        public string? Message { get; set; }
    }
}
