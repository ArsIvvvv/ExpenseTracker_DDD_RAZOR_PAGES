using ExpenseTracker.Domain.AvailableEntities;
using ExpenseTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Domain.Repository
{
    public interface IExpenseRepositoty
    {
        Task AddAsync(Expense expense);

        Task DeleteAsync(Expense expense);

        Task<IEnumerable<Expense>> GetAllAsync();

        Task <Expense?> GetExpenseById(Guid id);

        Task<List<string>>GetAllAvailableCategoriesAsync();


    }
}
