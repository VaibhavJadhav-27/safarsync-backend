using Dapper;
using SafarSync.API.Entities;
using SafarSync.API.Interfaces;

namespace SafarSync.API.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IDbConnectionFactory _dbFactory;

        public TaskRepository(IDbConnectionFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task AddTask(TaskItem task)
        {
            try
            {
                using var connection = _dbFactory.CreateConnection();

                var sql = @"INSERT INTO tasks (id, trip_id, title, assigned_to, due_date, created_by) VALUES (@Id, @TripId, @Title, @AssignedTo, @DueDate, @CreatedBy);";

                await connection.ExecuteAsync(sql, task);
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw new Exception("An error occurred while adding the task.", ex);
            }
        }

        public async Task DeleteTask(Guid taskId)
        {
            try
            {
                using var connection = _dbFactory.CreateConnection();

                await connection.ExecuteAsync("DELETE FROM tasks WHERE id=@TaskId", new { TaskId = taskId });
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw new Exception("An error occurred while deleting the task.", ex);
            }
        }

        public async Task<IEnumerable<TaskItem>> GetTasksByTrip(Guid tripId)
        {
            try
            {
                using var connection = _dbFactory.CreateConnection();

                return await connection.QueryAsync<TaskItem>("SELECT * FROM tasks WHERE trip_id=@TripId", new { TripId = tripId });
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw new Exception("An error occurred while retrieving tasks for the trip.", ex);
            }
        }

        public async Task MarkComplete(Guid taskId)
        {
            try
            {
                using var connection = _dbFactory.CreateConnection();

                await connection.ExecuteAsync("UPDATE tasks SET is_completed=true WHERE id=@TaskId", new { TaskId = taskId });
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw new Exception("An error occurred while marking the task as complete.", ex);
            }
        }
    }
}
