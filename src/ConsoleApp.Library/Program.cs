using DataAccessLayer.Library;
using Model.Library;
using System.Data;

//Book book1 = new()
//{
//    BookId = 1,
//    Quantity = 1,
//    Anagraphic = new()
//    {
//        Title = "Star Wars",
//        PublishingHouse = "Pippo",
//        Author = new()
//        {
//            Name = "Ciccio",
//            Surname = "Nonna Papera"
//        }
//    }
//};

//Book book2 = new()
//{
//    BookId = 2,
//    Quantity = 1,
//    Anagraphic = new()
//    {
//        Title = "Star Trek",
//        PublishingHouse = "Franco",
//        Author = new()
//        {
//            Name = "Topolino",
//            Surname = "Paperino"
//        }
//    }
//};

//Book book3 = new()
//{
//    BookId = 3,
//    Quantity = 1,
//    Anagraphic = new()
//    {
//        Title = "Star Trek",
//        PublishingHouse = "Franco",
//        Author = new()
//        {
//            Name = "Topolino",
//            Surname = "Paperino"
//        }
//    }
//};

//Console.WriteLine(book1.Equals(book2));
//Console.WriteLine(book2.Equals(book3));
//Console.WriteLine("book1 hash: " + book1.GetHashCode());
//Console.WriteLine("book2 hash: " + book2.GetHashCode());
//Console.WriteLine("book3 hash: " + book3.GetHashCode());

////-------------------------------------------------------------
//List<Book> books = new()
//{
//    book1,
//    book2,
//    book3,
//};

//XmlSerializer serializer = new XmlSerializer(typeof(List<Book>));
//User user1 = new User()
//{
//    UserId = 1,
//    Username = "pippo",
//    Password = "123456",
//    Role = Role.Admin,
//};

//User user2 = new User()
//{
//    UserId = 2,
//    Username = "franco",
//    Password = "123456",
//    Role = Role.User,
//};
//List<User> users = new() { user1, user2 };


//XmlSerializer serializer = new XmlSerializer(typeof(List<User>));
//using StreamWriter sw = new("users.xml");
//serializer.Serialize(sw, users);
//XmlWriterSettings settings = new();
//settings.Indent = true;

//using XmlWriter xmlWriter = XmlWriter.Create("users.xml", settings);

//using StreamReader sr = new("users.xml");
//var users2 = serializer.Deserialize(sr) as List<User>;
//Console.WriteLine(users2?.Count);
//users2.ForEach(user =>  Console.WriteLine(user.UserId));

// Creazione di un oggetto StreamWriter per scrivere il file XML
//using StreamWriter sw = new StreamWriter("books.xml");
//serializer.Serialize(sw, books);
//sw.Close();

//using StreamReader sr = new("books.xml");

//List<Book>? books2 = serializer.Deserialize(sr) as List<Book>;
//Console.WriteLine(books2?.Count);


//string xmlFilePath = "books.xml";

// Creazione di un nuovo DataSet
//DataSet dataSet = new DataSet();

// Caricamento del file XML esistente, se presente
//if (System.IO.File.Exists(xmlFilePath))
//{
//    dataSet.ReadXml(xmlFilePath);
//}
//else
//{
//    // Se il file non esiste, creiamo una nuova tabella nel DataSet
//    DataTable dataTable = new DataTable("Book");
//    dataTable.Columns.Add("BookId", typeof(Guid));
//    dataTable.Columns.Add("Anagraphic_Title", typeof(string));
//    dataTable.Columns.Add("Anagraphic_Author_Name", typeof(string));
//    dataTable.Columns.Add("Anagraphic_Author_Surname", typeof(string));
//    dataTable.Columns.Add("Anagraphic_PublishingHouse", typeof(string));
//    dataTable.Columns.Add("Quantity", typeof(int));
//    dataSet.Tables.Add(dataTable);
//}

// Aggiunta del nuovo elemento al DataSet
//DataRow newRow = dataSet.Tables["Book"].NewRow();
//newRow["BookId"] = Guid.NewGuid();
//newRow["Anagraphic_Title"] = "pippo3";
//newRow["Anagraphic_Author_Name"] = "pippis3";
//newRow["Anagraphic_Author_Surname"] = "de pippis3";
//newRow["Anagraphic_PublishingHouse"] = "casa del cà3";
//newRow["Quantity"] = 53;
//dataSet.Tables["Book"].Rows.Add(newRow);

// Scrittura del DataSet nel file XML
//dataSet.WriteXml(xmlFilePath, XmlWriteMode.WriteSchema);


//string xmlFilePath2 = "books.xml";

//// Creazione di un nuovo DataSet
//DataSet dataSet2 = new DataSet();

//// Leggere i dati dal file XML
//dataSet2.ReadXml(xmlFilePath2);

//// Verifica se il DataSet contiene dati
//if (dataSet2.Tables.Count > 0)
//{
//    // Accesso alla tabella Book
//    DataTable dataTable = dataSet2.Tables["Book"];

//    // Stampa dei dati
//    foreach (DataRow row in dataTable.Rows)
//    {
//        Console.WriteLine($"BookId: {row["BookId"]}");
//        Console.WriteLine($"Title: {row["Anagraphic_Title"]}");
//        Console.WriteLine($"Author: {row["Anagraphic_Author_Name"]} {row["Anagraphic_Author_Surname"]}");
//        Console.WriteLine($"Publishing House: {row["Anagraphic_PublishingHouse"]}");
//        Console.WriteLine($"Quantity: {row["Quantity"]}");
//        Console.WriteLine();
//    }
//}
//else
//{
//    Console.WriteLine("Nessun dato trovato nel file XML.");
//}
//DataAccess<Book> daBook = new();
//var propsBook = daBook.GetProperties();
//var classTypeBook = daBook.GetClassType();

//Console.WriteLine(classTypeBook);
//foreach (var item in propsBook)
//{
//    Console.WriteLine(item.Name + " " + item.PropertyType.Name);

//}

//DataAccess<User> daUser = new();
//var propsUser = daUser.GetProperties();
//var classTypeUser = daUser.GetClassType();

//Console.WriteLine(classTypeUser);
//foreach (var item in propsUser)
//{
//    Console.WriteLine(item.Name + " " + item.PropertyType);
//}

//DataAccess<Book> dataAccess = new DataAccess<Book>();
//Book book = new()
//{
//    Name = "Pippo7",
//    Title = "PippoLibro",
//    Surname = "De pippis",
//    PublishingHouse = "Konami",
//    Quantity = 10,
//    BookId = Guid.NewGuid(),
//};

//Book book2 = new()
//{
//    Name = "Pippo8",
//    Title = "PippoLibro2",
//    Surname = "De pippis2",
//    PublishingHouse = "Konami2",
//    Quantity = 10,
//    BookId = Guid.NewGuid()
//};

//var data = dataAccess.ConvertListToDataSet(new() { book, book2 });
//data.WriteXml(dataAccess.GetClassType() + ".xml", XmlWriteMode.WriteSchema);

//DataAccess<User> dataAccess = new DataAccess<User>();
//DataSet dataSet = new DataSet();
//dataSet.ReadXml("User.xml");
//var list = dataAccess.ConvertDataSetToList(dataSet);
//list.ForEach(item => Console.WriteLine(item.Username));
//User u1 = new User()
//{
//    UserId = Guid.NewGuid(),
//    Username = "TestUser",
//    Password = "123456",
//    Role = Role.Admin
//};

//User u2 = new User()
//{
//    UserId = Guid.NewGuid(),
//    Username = "TestUser2",
//    Password = "24680",
//    Role = Role.User
//};

//var data = dataAccess.ConvertListToDataSet(new() { u1, u2 });
//data?.WriteXml(dataAccess.GetClassType() + ".xml", XmlWriteMode.WriteSchema);

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

var data = dataAccess.AddListToDataSet([r1, r2]);
//var data = dataAccess.AddItemToDataSet(r1);
data?.WriteXml(dataAccess.GetClassType() + ".xml", XmlWriteMode.WriteSchema);


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