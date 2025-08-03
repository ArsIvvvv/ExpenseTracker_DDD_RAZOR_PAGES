using ExpenseTracker.Application.DTO;
using ExpenseTracker.Application.DTO.Command;
using ExpenseTracker.Application.ErrorHandler;
using ExpenseTracker.Application.Interface;
using ExpenseTracker.Domain.AvailableEntities;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Repository;
using ExpenseTracker.Domain.Value_Object;
using ExpenseTracker.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExpenseTracker.Application.Service
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepositoty _repositoty;

        public ExpenseService(IExpenseRepositoty repositoty)
        {
            _repositoty = repositoty;
        }

        
        public async Task <Result<ExpenseDto>> AddExpenseAsync(CreateExpenseCommand command, CancellationToken cancellationToken)
        {
            var ex = new Expense(command.Description, new Money(command.Amount),new Category(command.Category), command.Date);
            await _repositoty.AddAsync(ex,cancellationToken);

            var expense = new ExpenseDto
            {
                Id = ex.Id,
                Description = ex.Description,
                Money = ex.Money,
                Category = ex.Category,
                Date = ex.Date
            };

            return Result<ExpenseDto>.Success(expense);
        }

        public async Task<Result<ExpenseDto>> Delete(Guid expenseId, CancellationToken cancellationToken)
        {
            var ex = await _repositoty.GetExpenseById(expenseId,cancellationToken);
            if (ex == null)
                return Result<ExpenseDto>.Failure("Нету такого расходы по Id");

            await _repositoty.DeleteAsync(ex,  cancellationToken);
            var expense =  new ExpenseDto
            {
                Id = ex.Id,
                Description = ex.Description,
                Money = ex.Money,
                Category = ex.Category,
                Date = ex.Date
            };

            return Result<ExpenseDto>.Success(expense);

        }

        public async Task <Result<List<string>>> GetAllCategory(CancellationToken cancellationToken)
        {
           var list =  await _repositoty.GetAllAvailableCategoriesAsync(cancellationToken);
            if (list == null)
                return Result<List<string>>.Failure("Cписок категорий пуст");

            return Result<List<string>>.Success(list);
        }

        public async Task <Result<IEnumerable<ExpenseDto>>> GetAllExpensesAsync(CancellationToken cancellationToken)
        {

            var ex = await _repositoty.GetAllAsync(cancellationToken);
            if (ex == null)
                return Result<IEnumerable<ExpenseDto>>.Failure("Список расходов пуст");


            var expense =  ex.Select(e => new ExpenseDto 
            { 
                Id = e.Id,
                Description = e.Description,
                Money = e.Money,
                Category = e.Category,
                Date = e.Date
           });

            return Result<IEnumerable<ExpenseDto>>.Success(expense);
            
        }

        
    }
}
