using ExpenseTracker.Domain.AvailableEntities;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Repository;
using ExpenseTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Infrastructure.Repository
{
    public class ExpenseRepository : IExpenseRepositoty
    {
        private readonly ExpenseDbContext _dbContext;

        public ExpenseRepository(ExpenseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Expense expense, CancellationToken cancellationToken)
        {
            await _dbContext.Expenses.AddAsync(expense); 
             await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Expense expense, CancellationToken cancellationToken)
        {
             _dbContext.Expenses.Remove(expense);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Expense>?> GetAllAsync(CancellationToken cancellationToken)
        {
           return await _dbContext.Expenses.ToListAsync(cancellationToken);
        }

        public async Task<List<string>> GetAllAvailableCategoriesAsync(CancellationToken cancellationToken)
        {
             return await _dbContext.AvailableCategories.Select(c => c.Name).AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<Expense?> GetExpenseById(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.Expenses.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
    }
}
