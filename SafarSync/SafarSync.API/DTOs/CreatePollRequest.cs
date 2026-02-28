namespace SafarSync.API.DTOs
{
    public class CreatePollRequest
    {
        public Guid TripId { get; set; }
        public string Question { get; set; } = string.Empty;
        public List<string> Options { get; set; } = new();
        public Guid CreatedBy { get; set; }
    }
}
