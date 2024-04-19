namespace BusinessLogic.Library
{
    public class ReservationResult
    {
        public required string Title { get; set; }
        public required string Username { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}
