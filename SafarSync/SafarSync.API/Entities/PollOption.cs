namespace SafarSync.API.Entities
{
    public class PollOption
    {
        public Guid Id { get; set; }
        public Guid PollId { get; set; }
        public string OptionText { get; set; } = string.Empty;
    }
}
