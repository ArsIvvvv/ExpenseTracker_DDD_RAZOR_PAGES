using ExpenseTracker.Application.DTO;
using ExpenseTracker.Application.DTO.Command;
using ExpenseTracker.Application.ErrorHandler;
using ExpenseTracker.Domain.AvailableEntities;
using ExpenseTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application.Interface
{
    public interface IExpenseService
    {

        /// <summary>
        /// Добавляет расход.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result<ExpenseDto>> AddExpenseAsync(CreateExpenseCommand command, CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает список IEnumerable расходов.
        /// В случае неправильно введенных данных, возвращает результат ошибки с сообщением.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result<IEnumerable<ExpenseDto>>> GetAllExpensesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет расход.
        /// </summary>
        /// <param name="expenseId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result<ExpenseDto>> Delete(Guid expenseId, CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает список категорий.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>List<string></returns>
        Task<Result<List<string>>> GetAllCategory(CancellationToken cancellationToken);


    }
}
