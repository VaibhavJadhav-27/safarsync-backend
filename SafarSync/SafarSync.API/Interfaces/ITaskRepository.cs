using SafarSync.API.Entities;

namespace SafarSync.API.Interfaces
{
    public interface ITaskRepository
    {
        Task AddTask(TaskItem task);
        Task<IEnumerable<TaskItem>> GetTasksByTrip(Guid tripId);
        Task MarkComplete(Guid taskId);
        Task DeleteTask(Guid taskId);
    }
}
