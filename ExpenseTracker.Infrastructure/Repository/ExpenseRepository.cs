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

        public async Task AddAsync(Expense expense)
        {
            await _dbContext.Expenses.AddAsync(expense); 
             await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Expense expense)
        {
             _dbContext.Expenses.Remove(expense);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Expense>?> GetAllAsync()
        {
           return await _dbContext.Expenses.ToListAsync();
        }

        public async Task<List<string>> GetAllAvailableCategoriesAsync()
        {
             return await _dbContext.AvailableCategories.Select(c => c.Name).ToListAsync();
        }

        public async Task<Expense?> GetExpenseById(Guid id)
        {
            return await _dbContext.Expenses.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
