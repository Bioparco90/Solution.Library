namespace Model.Library
{
    public class User
    {
        private List<string> AcceptedRoles = ["Admin", "User"];
        public Guid UserId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }

        private string _role = string.Empty;
        public string Role
        {
            get => _role;
            set
            {
                if (!AcceptedRoles.Contains(value))
                {
                    value = "Undefined Role";
                }
                _role = value;
            }
        }

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
