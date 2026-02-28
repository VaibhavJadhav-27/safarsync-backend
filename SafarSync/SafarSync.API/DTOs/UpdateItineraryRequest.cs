namespace SafarSync.API.DTOs
{
    public class UpdateItineraryRequest
    {
        public int DayNumber { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public TimeSpan? Time { get; set; }
    }
}
