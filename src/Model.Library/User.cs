namespace Model.Library
{
    public class User
    {
        private static int Id = 1;
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public Role Role { get; set; }


        public User() => UserId = Id++;
    }
}
