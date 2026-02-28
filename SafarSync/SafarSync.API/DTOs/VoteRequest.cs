namespace SafarSync.API.DTOs
{
    public class VoteRequest
    {
        public Guid UserId { get; set; }
        public Guid OptionId { get; set; }
    }
}
