using Model.Library;
using System.Data.SqlClient;

namespace DataAccessLayer.Library.DAO
{
    public class ReservationDAO
    {
        private readonly DatabaseContext _db;

        public ReservationDAO(DatabaseContext db)
        {
            _db = db;
        }

        public bool Create(Guid userId, Guid bookId)
        {
            return _db.DoWithOpenConnection(conn =>
            {
                string commandString = "INSERT INTO Reservations(UserId, BookId, StartDate, EndDate) VALUES (@userId, @bookId, @startDate, @endDate)";
                SqlCommand cmd = new(commandString, conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@bookId", bookId);
                cmd.Parameters.AddWithValue("@startDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@endDate", DateTime.Now.AddDays(30));
                var rows = cmd.ExecuteNonQuery();

                return rows == 1;
            });
        }

        public IEnumerable<Reservation> GetAll()
        {
            return _db.DoWithOpenConnection(conn =>
            {
                string commandString = "SELECT * FROM Reservations";
                SqlCommand cmd = new(commandString, conn);
                using SqlDataReader data = cmd.ExecuteReader();
                List<Reservation> reservations = new();
                while (data.Read())
                {
                    Reservation book = new Reservation()
                    {
                        Id = (Guid)data["ID"],
                        UserId = (Guid)data["UserId"],
                        BookId = (Guid)data["BookId"],
                        StartDate = (DateTime)data["StartDate"],
                        EndDate = (DateTime)data["EndDate"],
                    };

                    reservations.Add(book);
                }
                return reservations;
            });
        }
    }
}
