using System.Runtime.InteropServices.JavaScript;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAcess.Repositories
{
    internal class ExpensesRepository : IExpensesReadOnlyRepository, IExpensesWriteOnlyRepository,
        IExpensesUpdateOnlyRepository
    {
        private readonly CashFlowDbContext _dbContext;

        public ExpensesRepository(CashFlowDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Expense expense)
        {
            await _dbContext.Expenses.AddAsync(expense);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var result = await _dbContext.Expenses.FirstOrDefaultAsync(expense => expense.Id == id);
            if (result is null)
            {
                return false;
            }

            _dbContext.Expenses.Remove(result);

            return true;
        }

        public async Task<List<Expense>> GetAllAsync()
        {
            return await _dbContext.Expenses.AsNoTracking().ToListAsync();
        }

        async Task<Expense?> IExpensesReadOnlyRepository.GetByIdAsync(long id)
        {
            return await _dbContext.Expenses.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        async Task<Expense?> IExpensesUpdateOnlyRepository.GetByIdAsync(long id)
        {
            return await _dbContext.Expenses.FirstOrDefaultAsync(e => e.Id == id);
        }

        public void Update(Expense expense)
        {
            _dbContext.Expenses.Update(expense);
        }

        public async Task<List<Expense>> FilterByMonthAsync(DateOnly date)
        {
            var startDate = new DateTime(year: date.Year, month: date.Month, day: 1).Date;
            var daysInMounth = DateTime.DaysInMonth(year: date.Year, month: date.Month);
            var endDate = new DateTime(year: date.Year, month: date.Month, day: daysInMounth, hour: 23, minute: 59,
                second: 59);

            return await _dbContext
                .Expenses
                .AsNoTracking()
                .Where(expense => expense.Date >= startDate && expense.Date <= endDate)
                .OrderBy(expense => expense.Date)
                .ThenBy(expense => expense.Title)
                .ToListAsync();
        }
    }
}