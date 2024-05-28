using DataAccessLayer.Library.DAO.Interfaces;
using Model.Library;
using System.Data.SqlClient;

namespace DataAccessLayer.Library.DAO
{
    public class ReservationDAO : IReservationDAO
    {
        private readonly IOpenConnection _db;

        public ReservationDAO(IOpenConnection db)
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

        public IEnumerable<Reservation> GetAll()
        {
            return _db.DoWithOpenConnection(conn =>
            {
                string commandString = "SELECT ID, UserId, BookId, StartDate, EndDate FROM Reservations";
                return RetrieveData(commandString, conn, BuildReservations);
            });
        }

        public IEnumerable<HumanReadableReservation> GetAllReadable()
        {
            return _db.DoWithOpenConnection(conn =>
            {
                string commandString = "SELECT Title, Username, StartDate, EndDate, Status FROM ReservationsCrossWithStatus";
                return RetrieveData(commandString, conn, BuildReadableReservationsWithStatus);
            });
        }

        public IEnumerable<HumanReadableReservation> GetAllReadable(string username)
        {
            return _db.DoWithOpenConnection(conn =>
            {
                Dictionary<string, object> properties = new() { { "Username", username } };
                string filter = BuilderUtilities.CreateFilterString(properties);
                string commandString = $"SELECT Title, Username, StartDate, EndDate, Status FROM ReservationsCrossWithStatus WHERE {filter}";
                return RetrieveData(commandString, conn, properties, BuildReadableReservationsWithStatus);
            });
        }

        public IEnumerable<HumanReadableReservation> GetByProperties(Dictionary<string, object> properties)
        {
            return _db.DoWithOpenConnection(conn =>
            {
                if (properties.Count == 0)
                {
                    return GetAllReadable();
                }

                string filter = BuilderUtilities.CreateFilterString(properties);
                string commandString = $"SELECT Title, Username, StartDate, EndDate, Status FROM ReservationsCrossWithStatus WHERE {filter}";
                return RetrieveData(commandString, conn, properties, BuildReadableReservationsWithStatus);
            });
        }

        public IEnumerable<HumanReadableReservation> GetActives()
        {
            return _db.DoWithOpenConnection(conn =>
            {
                string commandString = $"SELECT ID, Username, Title, StartDate, EndDate FROM ActiveReservationsCross";
                return RetrieveData(commandString, conn, BuildReadableReservationsWithId);
            });
        }

        public IEnumerable<HumanReadableReservation> GetActives(Guid bookId)
        {
            return _db.DoWithOpenConnection(conn =>
            {
                string commandString = $"SELECT ID, Username, Title, StartDate, EndDate FROM ActiveReservationsCross WHERE BookId = @bookId";
                return RetrieveData(commandString, conn, bookId, BuildReadableReservationsWithId);
            });
        }

        private IEnumerable<T> RetrieveData<T>(string commandString, SqlConnection conn, Dictionary<string, object> properties, Func<SqlDataReader, IEnumerable<T>> retrieved)
        {
            SqlCommand cmd = new(commandString, conn);
            BuilderUtilities.AddFilterParameters(cmd, properties);
            using SqlDataReader data = cmd.ExecuteReader();
            return retrieved(data);
        }

        private IEnumerable<T> RetrieveData<T>(string commandString, SqlConnection conn, Func<SqlDataReader, IEnumerable<T>> retrieved)
        {
            SqlCommand cmd = new(commandString, conn);
            using SqlDataReader data = cmd.ExecuteReader();
            return retrieved(data);
        }

        private IEnumerable<T> RetrieveData<T>(string commandString, SqlConnection conn, Guid bookId, Func<SqlDataReader, IEnumerable<T>> retrieved)
        {
            SqlCommand cmd = new(commandString, conn);
            cmd.Parameters.AddWithValue("@bookId", bookId);
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

        private HumanReadableReservation Build(SqlDataReader data)
        {
            return new()
            {
                Username = data["Username"] as string ?? string.Empty,
                Title = data["Title"] as string ?? string.Empty,
                StartDate = (DateTime)data["StartDate"],
                EndDate = (DateTime)data["EndDate"],
            };
        }

        private IEnumerable<HumanReadableReservation> BuildReadableReservations(SqlDataReader data, Action<HumanReadableReservation, SqlDataReader>? additionalField = null)
        {
            List<HumanReadableReservation> reservations = new();
            while (data.Read())
            {
                HumanReadableReservation reservation = Build(data);
                additionalField?.Invoke(reservation, data);
                reservations.Add(reservation);
            }
            return reservations;
        }

        private IEnumerable<HumanReadableReservation> BuildReadableReservationsWithId(SqlDataReader data)
        {
            return BuildReadableReservations(data, (reservation, dataReader) =>
            {
                reservation.Id = (Guid)data["ID"];
            });
        }

        private IEnumerable<HumanReadableReservation> BuildReadableReservationsWithStatus(SqlDataReader data)
        {
            return BuildReadableReservations(data, (reservation, dataReader) =>
            {
                reservation.Status = data["Status"] as string ?? string.Empty;
            });
        }
    }
}
