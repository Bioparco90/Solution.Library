using Model.Library.Enums;

namespace Model.Library
{
    public class User : DataObject
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public Role Role { get; set; }
    }
}
