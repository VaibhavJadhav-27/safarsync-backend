namespace SafarSync.API.Entities
{
    public class TaskItem
    {
        public Guid Id { get; set; }
        public Guid TripId { get; set; }
        public string Title { get; set; } = string.Empty;
        public Guid? AssignedTo { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
