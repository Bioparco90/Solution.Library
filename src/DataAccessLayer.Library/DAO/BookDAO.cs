﻿using DataAccessLayer.Library.DAO.Interfaces;
using Model.Library;
using System.Data.SqlClient;

namespace DataAccessLayer.Library.DAO
{
    public class BookDAO : IBookDAO
    {
        private readonly IOpenConnection _db;

        public BookDAO(IOpenConnection db)
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

                using SqlDataReader data = cmd.ExecuteReader();
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

        public bool Delete(Guid id)
        {
            return _db.DoWithOpenConnection(conn =>
            {
                string commandString = "DELETE FROM Books WHERE ID=@id";
                SqlCommand cmd = new(commandString, conn);
                cmd.Parameters.AddWithValue("@id", id);

                var rows = cmd.ExecuteNonQuery();
                return rows >= 1;
            });
        }

        public IEnumerable<Book> GetAll()
        {
            return _db.DoWithOpenConnection(conn =>
            {
                string commandString = "SELECT * FROM Books";
                SqlCommand cmd = new(commandString, conn);
                using SqlDataReader data = cmd.ExecuteReader();
                List<Book> books = new();
                while (data.Read())
                {
                    Book book = new Book
                    {
                        Id = (Guid)data["ID"],
                        Title = data["Title"] as string ?? string.Empty,
                        AuthorName = data["AuthorName"] as string ?? string.Empty,
                        AuthorSurname = data["AuthorSurname"] as string ?? string.Empty,
                        PublishingHouse = data["PublishingHouse"] as string ?? string.Empty,
                        Quantity = (int)data["Quantity"]
                    };

                    books.Add(book);
                }
                return books;
            });
        }

        public IEnumerable<Book> GetByProperties(Dictionary<string, object> properties)
        {
            return _db.DoWithOpenConnection(conn =>
            {
                if (properties.Count == 0)
                {
                    return GetAll();
                }

                string filter = BuilderUtilities.CreateFilterString(properties);
                string commandString = $"SELECT * FROM Books WHERE{filter}";

                SqlCommand cmd = new(commandString, conn);
                BuilderUtilities.AddFilterParameters(cmd, properties);
                using SqlDataReader data = cmd.ExecuteReader();
                List<Book> books = new();
                while (data.Read())
                {
                    Book book = new Book
                    {
                        Id = (Guid)data["ID"],
                        Title = data["Title"] as string ?? string.Empty,
                        AuthorName = data["AuthorName"] as string ?? string.Empty,
                        AuthorSurname = data["AuthorSurname"] as string ?? string.Empty,
                        PublishingHouse = data["PublishingHouse"] as string ?? string.Empty,
                        Quantity = (int)data["Quantity"]
                    };

                    books.Add(book);
                }
                return books;
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
