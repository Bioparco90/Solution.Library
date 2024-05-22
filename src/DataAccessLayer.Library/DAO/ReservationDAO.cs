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
                string commandString = "SELECT ID, UserId, BookId, StartDate, EndDate FROM Reservations";
                return RetrieveData(commandString, conn, BuildReservations);
            });
        }

        public bool Update(Guid id, Dictionary<string, object> parameters)
        {
            return _db.DoWithOpenConnection(conn =>
            {
                string projection = BuilderUtilities.CreateString(parameters);
                string commandString = $"UPDATE Reservations SET{projection} WHERE ID=@id";

                SqlCommand cmd = new(commandString, conn);
                BuilderUtilities.AddParameters(cmd, parameters);
                cmd.Parameters.AddWithValue("@id", id);
                var rows = cmd.ExecuteNonQuery();

                return rows == 1;
            });
        }

        public IEnumerable<ActiveReservation> GetActives()
        {
            return _db.DoWithOpenConnection(conn =>
            {
                string commandString = $"SELECT Username, Title, StartDate, EndDate FROM ActiveReservationsCross";
                return RetrieveData(commandString, conn, BuildActiveReservations);
            });
        }

        private IEnumerable<T> RetrieveData<T>(string commandString, SqlConnection conn, Func<SqlDataReader, IEnumerable<T>> retrieved)
        {
            SqlCommand cmd = new(commandString, conn);
            using SqlDataReader data = cmd.ExecuteReader();
            return retrieved(data);
        }

        private IEnumerable<Reservation> BuildReservations(SqlDataReader data)
        {
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
        }

        private IEnumerable<ActiveReservation> BuildActiveReservations(SqlDataReader data)
        {
            List<ActiveReservation> reservations = new();
            while (data.Read())
            {
                ActiveReservation book = new ActiveReservation()
                {
                    Username = data["Username"] as string ?? string.Empty,
                    Title = data["Title"] as string ?? string.Empty,
                    StartDate = (DateTime)data["StartDate"],
                    EndDate = (DateTime)data["EndDate"],
                };

                reservations.Add(book);
            }
            return reservations;
        }
    }
}
