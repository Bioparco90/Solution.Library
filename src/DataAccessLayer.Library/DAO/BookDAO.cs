using Model.Library;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace DataAccessLayer.Library.DAO
{
    public class BookDAO
    {
        private DatabaseContext _db;

        public BookDAO(DatabaseContext db)
        {
            _db = db;
        }

        public bool Add(Book book)
        {
            // Insert into Books values ...
            return _db.DoWithOpenConnection(conn =>
            {
                string commandString = "INSERT INTO Books(Title, AuthorName, AuthorSurname, PublishingHouse, Quantity) VALUES(@title, @authorName, @authorSurname, @publishingHouse, @quantity)";
                SqlCommand cmd = CreateCommand(book, commandString, conn);
                var rows = cmd.ExecuteNonQuery();
                return rows == 1;
            });
        }



        public bool Update(Book book)
        {
            return _db.DoWithOpenConnection(conn =>
            {
                string commandString = "UPDATE Books SET Title=@title, AuthorName=@authorName, AuthorSurname=@authorSurname, PublishingHouse=@publishingHouse, Quantity=@quantity WHERE ID=@id";
                SqlCommand cmd = CreateCommand(book, commandString, conn);
                cmd.Parameters.AddWithValue("@id", book.Id);
                var rows = cmd.ExecuteNonQuery();
                return rows == 1;
            });
        }

        public Book? GetById(Guid id)
        {
            return _db.DoWithOpenConnection(conn =>
            {
                string commandString = "SELECT * FROM Books WHERE ID=@id";
                SqlCommand cmd = new(commandString, conn);
                cmd.Parameters.AddWithValue("@id", id);

                var data = cmd.ExecuteReader();
                if (data.Read())
                {
                    return new Book()
                    {
                        Id = (Guid)data["ID"],
                        Title = data["Title"] as string ?? string.Empty,
                        AuthorName = data["AuthorName"] as string ?? string.Empty,
                        AuthorSurname = data["AuthorSurname"] as string ?? string.Empty,
                        PublishingHouse = data["PublishingHouse"] as string ?? string.Empty,
                        Quantity = (int)data["Quantity"]
                    };
                }
                return null;
            });
        }

        private static SqlCommand CreateCommand(Book book, string commandString, SqlConnection conn)
        {
            SqlCommand cmd = new(commandString, conn);
            cmd.Parameters.AddWithValue("@title", book.Title);
            cmd.Parameters.AddWithValue("@authorName", book.AuthorName);
            cmd.Parameters.AddWithValue("@authorSurname", book.AuthorSurname);
            cmd.Parameters.AddWithValue("@publishingHouse", book.PublishingHouse);
            cmd.Parameters.AddWithValue("@quantity", book.Quantity);
            return cmd;
        }
    }
}
