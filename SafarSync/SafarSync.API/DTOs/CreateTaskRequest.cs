namespace SafarSync.API.DTOs
{
    public class CreateTaskRequest
    {
        public Guid TripId { get; set; }
        public string Title { get; set; } = string.Empty;
        public Guid? AssignedTo { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
