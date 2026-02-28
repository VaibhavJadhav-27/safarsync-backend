namespace SafarSync.API.Entities
{
    public class ItineraryItem
    {
        public Guid Id { get; set; }
        public Guid TripId { get; set; }
        public int DayNumber { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public TimeSpan? Time { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
