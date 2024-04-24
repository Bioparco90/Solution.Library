using Model.Library;

Role role = default;
Role role2 = Role.None;
Role role3 = Role.Admin;

Console.WriteLine("1: " + (role == default));
Console.WriteLine("2: " + (role2 == default));
Console.WriteLine("3: " + (role3 == default));
