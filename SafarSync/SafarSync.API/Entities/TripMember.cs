namespace SafarSync.API.Entities
{
    public class TripMember
    {
        public Guid Id { get; set; }
        public Guid TripId { get; set; }
        public Guid UserId { get; set; }
        public string Role { get; set; } = "Member";
        public DateTime JoinedAt { get; set; }
    }
}
