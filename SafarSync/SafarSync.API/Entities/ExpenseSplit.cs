namespace SafarSync.API.Entities
{
    public class ExpenseSplit
    {
        public Guid Id { get; set; }
        public Guid ExpenseId { get; set; }
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
    }
}
