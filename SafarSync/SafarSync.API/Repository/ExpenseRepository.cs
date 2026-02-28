using Dapper;
using SafarSync.API.Entities;
using SafarSync.API.Interfaces;

namespace SafarSync.API.Repository
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly IDbConnectionFactory _dbFactory;

        public ExpenseRepository(IDbConnectionFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task AddExpense(Expense expense)
        {
            try
            {
                using var connection = _dbFactory.CreateConnection();

                var sql = @"INSERT INTO expenses (id, trip_id, paid_by, title, amount) VALUES (@Id, @TripId, @PaidBy, @Title, @Amount);";

                await connection.ExecuteAsync(sql, expense);
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
                Console.WriteLine($"Error adding expense: {ex.Message}");
                throw; // Re-throw the exception after logging
            }
        }

        public async Task AddExpenseSplit(ExpenseSplit split)
        {
            try
            {
                using var connection = _dbFactory.CreateConnection();

                var sql = @"INSERT INTO expense_splits (id, expense_id, user_id, amount) VALUES (@Id, @ExpenseId, @UserId, @Amount);";

                await connection.ExecuteAsync(sql, split);
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
                Console.WriteLine($"Error adding expense split: {ex.Message}");
                throw; // Re-throw the exception after logging
            }
        }

        public async Task<IEnumerable<Expense>> GetExpensesByTrip(Guid tripId)
        {
            try
            {
                using var connection = _dbFactory.CreateConnection();

                var sql = "SELECT * FROM expenses WHERE trip_id = @TripId;";

                return await connection.QueryAsync<Expense>(sql, new { TripId = tripId });
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
                Console.WriteLine($"Error retrieving expenses: {ex.Message}");
                throw; // Re-throw the exception after logging
            }
        }

        public async Task<IEnumerable<Guid>> GetTripMemberIds(Guid tripId)
        {
            try
            {
                using var connection = _dbFactory.CreateConnection();

                var sql = "SELECT user_id FROM trip_members WHERE trip_id = @TripId;";

                return await connection.QueryAsync<Guid>(sql, new { TripId = tripId });
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
                Console.WriteLine($"Error retrieving trip member IDs: {ex.Message}");
                throw; // Re-throw the exception after logging
            }
        }

        public async Task<IEnumerable<(Guid UserId, decimal NetAmount)>> GetTripBalances(Guid tripId)
        {
            try
            {
                using var connection = _dbFactory.CreateConnection();

                var sql = @"SELECT tm.user_id AS UserId, COALESCE(SUM(e.amount) FILTER (WHERE e.paid_by = tm.user_id), 0) - COALESCE(SUM(es.amount), 0) AS NetAmount
                            FROM trip_members tm
                            LEFT JOIN expenses e 
                                ON e.trip_id = tm.trip_id
                            LEFT JOIN expense_splits es 
                                ON es.expense_id = e.id 
                                AND es.user_id = tm.user_id
                            WHERE tm.trip_id = @TripId
                            GROUP BY tm.user_id;";

                var result = await connection.QueryAsync<(Guid, decimal)>(sql, new { TripId = tripId });

                return result.Select(r => (r.Item1, r.Item2));
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
                Console.WriteLine($"Error retrieving trip balances: {ex.Message}");
                throw; // Re-throw the exception after logging
            }
        }
    }
}
