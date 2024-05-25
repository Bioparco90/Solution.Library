namespace Model.Library
{
    public class HumanReadableReservation : DataObject
    {
        public string Username { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Status { get; set; }

        public override string ToString()
        {
            string statusInfo = string.IsNullOrEmpty(Status) ? "Expired" : Status;
            return $"{Title} – {Username} – {StartDate:dd/MM/yyyy} – {EndDate:dd/MM/yyyy} – {statusInfo}";
        }
    }
}
