namespace Model.Library
{
    public class Reservation : DataObject
    {
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Reservation reservation &&
                   UserId.Equals(reservation.UserId) &&
                   BookId.Equals(reservation.BookId) &&
                   StartDate == reservation.StartDate &&
                   EndDate == reservation.EndDate;
        }
    }
}
