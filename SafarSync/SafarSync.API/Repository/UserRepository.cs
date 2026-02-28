using Dapper;
using SafarSync.API.Entities;
using SafarSync.API.Interfaces;

namespace SafarSync.API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnectionFactory _dbFactory;

        public UserRepository(IDbConnectionFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }


        public async Task<User> CreateUser(User user)
        {
            try
            {
                using var connection = _dbFactory.CreateConnection();

                var query = "INSERT INTO Users (Id, FirebaseUserId, Name, Email, CreatedAt) VALUES (@Id, @FirebaseUserId, @Name, @Email, @CreatedAt) RETURNING *";

                return await connection.QuerySingleAsync<User>(query, user);
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
                Console.WriteLine($"Error creating user: {ex.Message}");
                throw; // Re-throw the exception to be handled by the caller
            }

        }

        public async Task<User?> GetByFirebaseId(string firebaseId)
        {
            try
            {
                using var connection = _dbFactory.CreateConnection();

                var sql = @"SELECT * FROM users WHERE firebase_user_id = @FirebaseId";

                return await connection.QueryFirstOrDefaultAsync<User>(sql, new { FirebaseId = firebaseId });
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
                Console.WriteLine($"Error fetching user by Firebase ID: {ex.Message}");
                throw; // Re-throw the exception to be handled by the caller
            }
        }
    }
}
