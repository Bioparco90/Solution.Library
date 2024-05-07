namespace Model.Library
{
    public class LoginResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public User User { get; set; }
    }
}
