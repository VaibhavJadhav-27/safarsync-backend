namespace SafarSync.API.DTOs
{
    public class CreateExpenseRequest
    {
        public Guid TripId { get; set; }
        public Guid PaidBy { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}
