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

        Task<IEnumerable<Expense>> GetAllAsync(Guid userId, CancellationToken cancellationToken);

        Task <Expense?> GetExpenseByUser(Guid userId, Guid id, CancellationToken cancellationToken);

        Task<List<string>>GetAllAvailableCategoriesAsync();


    }
}
