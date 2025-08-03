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
        /// <summary>
        /// Добаляет расход.
        /// </summary>
        /// <param name="expense"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task AddAsync(Expense expense, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет расход.
        /// </summary>
        /// <param name="expense"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DeleteAsync(Expense expense, CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает все расходы.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<Expense>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает расход по id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Expense</returns>
        Task<Expense?> GetExpenseById(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает категории для сортировки.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>List<string></returns>
        Task<List<string>>GetAllAvailableCategoriesAsync(CancellationToken cancellationToken);


    }
}
