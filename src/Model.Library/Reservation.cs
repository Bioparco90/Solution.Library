namespace Model.Library
{
    public class Reservation
    {
        private static int _id = 1;
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Reservation() => Id = _id++;
    }
}
