using SafarSync.API.Entities;

namespace SafarSync.API.Interfaces
{
    public interface IExpenseRepository
    {
        Task AddExpense(Expense expense);
        Task AddExpenseSplit(ExpenseSplit split);
        Task<IEnumerable<Expense>> GetExpensesByTrip(Guid tripId);
        Task<IEnumerable<Guid>> GetTripMemberIds(Guid tripId);
        Task<IEnumerable<(Guid UserId, decimal NetAmount)>> GetTripBalances(Guid tripId);
    }
}
