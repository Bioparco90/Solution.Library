namespace Model.Library
{
    public class Reservation : DataObject
    {
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
