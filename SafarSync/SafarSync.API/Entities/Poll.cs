namespace SafarSync.API.Entities
{
    public class Poll
    {
        public Guid Id { get; set; }
        public Guid TripId { get; set; }
        public string Question { get; set; } = string.Empty;
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
