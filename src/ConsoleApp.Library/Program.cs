using DataAccessLayer.Library;
using Model.Library;
using System.Data;


DataTableAccess<Reservation> dataAccess = new DataTableAccess<Reservation>();
Reservation r1 = new()
{
    BookId = Guid.NewGuid(),
    Id = Guid.NewGuid(),
    UserId = Guid.NewGuid(),
    StartDate = DateTime.Now,
    EndDate = DateTime.Now.AddDays(30),
};

Reservation r2 = new()
{
    BookId = Guid.NewGuid(),
    Id = Guid.NewGuid(),
    UserId = Guid.NewGuid(),
    StartDate = DateTime.Now,
    EndDate = DateTime.Now.AddDays(30),
};

var data = dataAccess.AddListToDataTable([r1, r2]);
//var data = dataAccess.AddItemToDataSet(r1);
data?.WriteXml(dataAccess.XMLFileName, XmlWriteMode.WriteSchema);


//DataAccess<User> dataAccess = new DataAccess<User>();
//DataSet dataSet = dataAccess.ReadDataSetFromFile("User.xml");
//var data = dataAccess.ConvertDataSetToList(dataSet);
//data.ForEach(item => Console.WriteLine(item.Username));

//User u1 = new User()
//{
//    UserId = Guid.NewGuid(),
//    Username = "TestUser2",
//    Password = "password",
//    Role = Role.Admin,
//};

//Console.WriteLine(data.Contains(u1));