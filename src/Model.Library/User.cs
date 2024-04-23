namespace Model.Library
{
    public class User : DataObject
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public Role Role { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is User user &&
                   Username == user.Username &&
                   Password == user.Password;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Username, Password);
        }
    }
}
