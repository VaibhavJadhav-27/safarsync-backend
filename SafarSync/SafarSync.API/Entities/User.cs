namespace SafarSync.API.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public string FirebaseUserId { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
