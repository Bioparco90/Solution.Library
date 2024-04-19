using Model.Library;

namespace BusinessLogic.Library
{
    public class SearchBooksParams
    {
        public string? Title { get; set; }
        public string? AuthorName { get; set; }
        public string? AuthorSurname { get; set; }
        public string? PublishingHouse { get; set; }
    }
}
