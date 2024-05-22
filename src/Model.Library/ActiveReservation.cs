namespace Model.Library
{
    public class ActiveReservation
    {
        public string Username { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public override string ToString()
        {
            return $"The cancellation was not carried out because the book {Title} is still reserved by the user {Username} from {StartDate:dd/MM/yyyy} to {EndDate:dd/MM/yyyy}.";
        }
    }
}
