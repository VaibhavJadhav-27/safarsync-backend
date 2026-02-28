using Dapper;
using SafarSync.API.DTOs;
using SafarSync.API.Entities;
using SafarSync.API.Interfaces;

namespace SafarSync.API.Repository
{
    public class ItineraryRepository : IItineraryRepository
    {
        private readonly IDbConnectionFactory _dbFactory;

        public ItineraryRepository(IDbConnectionFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task AddItem(ItineraryItem item)
        {
            try
            {
                using var connection = _dbFactory.CreateConnection();

                var sql = @"INSERT INTO itinerary_items (id, trip_id, day_number, title, description, time, created_by) VALUES (@Id, @TripId, @DayNumber, @Title, @Description, @Time, @CreatedBy);";

                await connection.ExecuteAsync(sql, item);
            }
            catch(Exception ex)
            {
                // Log the exception (not implemented here)
                throw new Exception("An error occurred while adding the itinerary item.", ex);
            }
        }

        public async Task DeleteItem(Guid id)
        {
            try
            {
                using var connection = _dbFactory.CreateConnection();

                await connection.ExecuteAsync("DELETE FROM itinerary_items WHERE id=@Id",new { Id = id });
            }
            catch(Exception ex)
            {
                // Log the exception (not implemented here)
                throw new Exception("An error occurred while deleting the itinerary item.", ex);
            }
        }

        public async Task<IEnumerable<ItineraryItem>> GetByTrip(Guid tripId)
        {
            try
            {
                using var connection = _dbFactory.CreateConnection();

                var sql = @"SELECT * FROM itinerary_items WHERE trip_id=@TripId ORDER BY day_number, time;";

                return await connection.QueryAsync<ItineraryItem>(sql, new { TripId = tripId });
            }
            catch(Exception ex)
            {
                // Log the exception (not implemented here)
                throw new Exception("An error occurred while retrieving itinerary items.", ex);
            }
        }

        public async Task UpdateItem(Guid id, UpdateItineraryRequest request)
        {
            try
            {
                using var connection = _dbFactory.CreateConnection();

                var sql = @"UPDATE itinerary_items SET day_number=@DayNumber,title=@Title,description=@Description,time=@Time WHERE id=@Id;";

                await connection.ExecuteAsync(sql,
                    new
                    {
                        Id = id,
                        request.DayNumber,
                        request.Title,
                        request.Description,
                        request.Time
                    });
            }
            catch(Exception ex)
            {
                // Log the exception (not implemented here)
                throw new Exception("An error occurred while updating the itinerary item.", ex);
            }
    }
}
