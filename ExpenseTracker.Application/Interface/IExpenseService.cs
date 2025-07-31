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
        Task <Result<ExpenseDto>> AddExpenseAsync(CreateExpenseCommand command);

        Task <Result<IEnumerable<ExpenseDto>>> GetAllExpensesAsync();

        Task <Result<ExpenseDto>> Delete(Guid expenseId);
        Task <Result<List<string>>> GetAllCategory();


    }
}
