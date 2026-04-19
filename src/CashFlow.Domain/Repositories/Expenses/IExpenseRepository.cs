using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses
{
    public interface IExpenseRepository
    {
        Task AddAsync(Expense expense);
        Task<List<Expense>> GetAllAsync();
        Task<Expense?> GetByIdAsync(long id);
    }
}
