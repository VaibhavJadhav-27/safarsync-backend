namespace SafarSync.API.DTOs
{
    public class SyncUserRequest
    {
        public string FirebaseUserId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
