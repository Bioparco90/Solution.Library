
namespace Model.Library
{
    public class Book : DataObject
    {
        public string? Title { get; set; }
        public string? AuthorName { get; set; }
        public string? AuthorSurname { get; set; }
        public string? PublishingHouse { get; set; }
        public int Quantity { get; set; }

        public override string ToString()
        {
            return $"Title: {Title ?? "N/A"}\n" +
                   $"Author Name: {AuthorName ?? "N/A"}\n" +
                   $"Author Surname: {AuthorSurname ?? "N/A"}\n" +
                   $"Publishing House: {PublishingHouse ?? "N/A"}\n" +
                   $"Quantity: {Quantity}";
        }



        public override bool Equals(object? obj)
        {
            return obj is Book book &&
                   Title == book.Title &&
                   AuthorName == book.AuthorName &&
                   AuthorSurname == book.AuthorSurname &&
                   PublishingHouse == book.PublishingHouse;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Title, AuthorName, AuthorSurname, PublishingHouse);
        }
    }
}
