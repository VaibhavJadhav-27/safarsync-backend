using Dapper;
using SafarSync.API.Entities;
using SafarSync.API.Interfaces;

namespace SafarSync.API.Repository
{
    public class TripRepository : ITripRepository
    {
        private readonly IDbConnectionFactory _dbFactory;

        public TripRepository(IDbConnectionFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task AddTripMember(TripMember member)
        {
            try
            {
                using var connection = _dbFactory.CreateConnection();

                var sql = @"INSERT INTO trip_members
                    (id, trip_id, user_id, role)
                    VALUES (@Id, @TripId, @UserId, @Role);";

                await connection.ExecuteAsync(sql, member);
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
                Console.WriteLine($"Error adding trip member: {ex.Message}");
                throw; // Re-throw the exception after logging
            }
        }

        public async Task<Trip> CreateTrip(Trip trip)
        {
            try
            {
                using var connection = _dbFactory.CreateConnection();

                var sql = @"INSERT INTO trips
                    (id, name, description, start_date, end_date, created_by)
                    VALUES (@Id, @Name, @Description, @StartDate, @EndDate, @CreatedBy)
                    RETURNING *;";

                return await connection.QuerySingleAsync<Trip>(sql, trip);
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
                Console.WriteLine($"Error creating trip: {ex.Message}");
                throw; // Re-throw the exception after logging
            }
        }

        public async Task<Trip?> GetTripById(Guid tripId)
        {
            try
            {
                using var connection = _dbFactory.CreateConnection();

                var sql = @"SELECT * FROM trips WHERE id = @TripId;";

                return await connection.QueryFirstOrDefaultAsync<Trip>(sql, new { TripId = tripId });
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
                Console.WriteLine($"Error retrieving trip by ID: {ex.Message}");
                throw; // Re-throw the exception after logging
            }
        }

        public async Task<IEnumerable<Trip>> GetTripsByUser(Guid userId)
        {
            try
            {
                using var connection = _dbFactory.CreateConnection();

                var sql = @"SELECT t.* FROM trips t
                    INNER JOIN trip_members tm ON t.id = tm.trip_id
                    WHERE tm.user_id = @UserId;";

                return await connection.QueryAsync<Trip>(sql, new { UserId = userId });
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
                Console.WriteLine($"Error retrieving trips for user: {ex.Message}");
                throw; // Re-throw the exception after logging
            }
        }
    }
}
