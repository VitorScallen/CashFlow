using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAcess.Repositories
{
    internal class ExpensesRepository : IExpenseRepository
    {
        private readonly CashFlowDbContext _dbContext;
        private IExpenseRepository _expenseRepositoryImplementation;

        public ExpensesRepository(CashFlowDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Expense expense)
        {
            await _dbContext.Expenses.AddAsync(expense);
        }

        public async Task<List<Expense>> GetAllAsync()
        {
            return await _dbContext.Expenses.AsNoTracking().ToListAsync();
        }

        public async Task<Expense?> GetByIdAsync(long id)
        {
            return await _dbContext.Expenses.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}