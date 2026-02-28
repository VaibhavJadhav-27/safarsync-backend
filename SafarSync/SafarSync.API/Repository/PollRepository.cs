using Dapper;
using SafarSync.API.Entities;
using SafarSync.API.Interfaces;

namespace SafarSync.API.Repository
{
    public class PollRepository : IPollRepository
    {
        private readonly IDbConnectionFactory _dbFactory;

        public PollRepository(IDbConnectionFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task AddOption(PollOption option)
        {
            try
            {
                using var connection = _dbFactory.CreateConnection();

                var sql = @"INSERT INTO poll_options (id, poll_id, option_text) VALUES (@Id, @PollId, @OptionText);";

                await connection.ExecuteAsync(sql, option);
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
                Console.WriteLine($"Error adding poll option: {ex.Message}");
                throw; // Re-throw the exception after logging
            }
        }

        public async Task AddPoll(Poll poll)
        {
            try
            {
                using var connection = _dbFactory.CreateConnection();

                var sql = @"INSERT INTO polls (id, trip_id, question, created_by) VALUES (@Id, @TripId, @Question, @CreatedBy);";

                await connection.ExecuteAsync(sql, poll);
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
                Console.WriteLine($"Error adding poll option: {ex.Message}");
                throw; // Re-throw the exception after logging
            }
        }

        public async Task AddVote(PollVote vote)
        {
            try
            {
                using var connection = _dbFactory.CreateConnection();

                var sql = @"INSERT INTO poll_votes (id, poll_id, option_id, user_id) VALUES (@Id, @PollId, @OptionId, @UserId);";

                await connection.ExecuteAsync(sql, vote);
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
                Console.WriteLine($"Error adding poll vote: {ex.Message}");
                throw; // Re-throw the exception after logging
            }
        }

        public async Task<IEnumerable<Poll>> GetPollsByTrip(Guid tripId)
        {
            try
            {
                using var connection = _dbFactory.CreateConnection();

                return await connection.QueryAsync<Poll>("SELECT * FROM polls WHERE trip_id=@TripId", new { TripId = tripId });
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
                Console.WriteLine($"Error retrieving polls: {ex.Message}");
                throw; // Re-throw the exception after logging
            }
        }

        public async Task<IEnumerable<(Guid OptionId, int VoteCount)>> GetResults(Guid pollId)
        {
            try
            {
                using var connection = _dbFactory.CreateConnection();

                var sql = @"SELECT option_id AS OptionId, COUNT(*) AS VoteCount
                    FROM poll_votes
                    WHERE poll_id=@PollId
                    GROUP BY option_id;";

                return await connection.QueryAsync<(Guid, int)>(sql,
                    new { PollId = pollId });
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
                Console.WriteLine($"Error retrieving poll results: {ex.Message}");
                throw; // Re-throw the exception after logging
            }
        }
    }
}
