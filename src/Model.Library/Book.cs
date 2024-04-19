
namespace Model.Library
{
    public class Book
    {
        private static int Id = 1;
        public int BookId { get; set; }
        public string? Title { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? PublishingHouse { get; set; }
        public int Quantity { get; set; }



        public Book() => BookId = Id++;

        public override bool Equals(object? obj)
        {
            return obj is Book book &&
                   Title == book.Title &&
                   Name == book.Name &&
                   Surname == book.Surname &&
                   PublishingHouse == book.PublishingHouse;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Title, Name, Surname, PublishingHouse);
        }
    }
}
