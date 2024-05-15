namespace Model.Library
{
    public class Book : DataObject
    {
        public required string Title { get; set; }
        public required string AuthorName { get; set; }
        public required string AuthorSurname { get; set; }
        public required string PublishingHouse { get; set; }
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
                   Title.ToLower() == book.Title.ToLower() &&
                   AuthorName.ToLower() == book.AuthorName.ToLower() &&
                   AuthorSurname.ToLower() == book.AuthorSurname.ToLower() &&
                   PublishingHouse.ToLower() == book.PublishingHouse.ToLower();
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Title, AuthorName, AuthorSurname, PublishingHouse);
        }
    }
}
