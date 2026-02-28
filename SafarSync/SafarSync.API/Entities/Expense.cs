namespace SafarSync.API.Entities
{
    public class Expense
    {
        public Guid Id { get; set; }
        public Guid TripId { get; set; }
        public Guid PaidBy { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
