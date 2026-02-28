using Microsoft.AspNetCore.Mvc;
using SafarSync.API.DTOs;
using SafarSync.API.Entities;
using SafarSync.API.Interfaces;

namespace SafarSync.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseRepository _expenseRepository;

        public ExpenseController(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        // ADD EXPENSE
        [HttpPost]
        public async Task<IActionResult> AddExpense(CreateExpenseRequest request)
        {
            if (request.Amount <= 0)
                return BadRequest("Amount must be greater than 0.");

            var expense = new Expense
            {
                Id = Guid.NewGuid(),
                TripId = request.TripId,
                PaidBy = request.PaidBy,
                Title = request.Title,
                Amount = request.Amount
            };

            await _expenseRepository.AddExpense(expense);

            var members = await _expenseRepository.GetTripMemberIds(request.TripId);

            var splitAmount = request.Amount / members.Count();

            foreach (var memberId in members)
            {
                var split = new ExpenseSplit
                {
                    Id = Guid.NewGuid(),
                    ExpenseId = expense.Id,
                    UserId = memberId,
                    Amount = splitAmount
                };

                await _expenseRepository.AddExpenseSplit(split);
            }

            return Ok("Expense added successfully");
        }

        // GET EXPENSES
        [HttpGet("trip/{tripId}")]
        public async Task<IActionResult> GetExpenses(Guid tripId)
        {
            var expenses = await _expenseRepository.GetExpensesByTrip(tripId);
            return Ok(expenses);
        }

        [HttpGet("trip/{tripId}/summary")]
        public async Task<IActionResult> GetTripSummary(Guid tripId)
        {
            var balances = await _expenseRepository.GetTripBalances(tripId);

            var result = balances.Select(b => new
            {
                userId = b.UserId,
                netAmount = b.NetAmount
            });

            return Ok(result);
        }
    }
}
