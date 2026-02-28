
namespace SafarSync.API.Entities
{
    public class CreateTripRequest
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // TEMP for development (will remove after Firebase auth)
        public Guid UserId { get; set; }
    }
}
