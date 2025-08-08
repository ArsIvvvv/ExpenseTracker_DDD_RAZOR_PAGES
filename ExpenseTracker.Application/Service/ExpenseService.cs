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
using System.Threading.Tasks;

namespace ExpenseTracker.Application.Service
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepositoty _repositoty;
        private readonly IUserRepository _userRepository;


        public ExpenseService(IExpenseRepositoty repositoty, IUserRepository userRepository)
        {
            _repositoty = repositoty;
            _userRepository = userRepository;
        }

        
        public async Task <Result<ExpenseDto>> AddExpenseAsync(CreateExpenseCommand command, CancellationToken cancellationToken)
        {

            var user = await _userRepository.GetByIdAsync(command.UserId, cancellationToken);

            var ex = new Expense(user, command.Description, new Money(command.Amount),new Category(command.Category), command.Date);

            await _repositoty.AddAsync(ex);

            var expense = new ExpenseDto
            {
                Id = ex.Id,
                UserId = ex.UserId,
                Description = ex.Description,
                Money = ex.Money,
                Category = ex.Category,
                Date = ex.Date
            };

            return Result<ExpenseDto>.Success(expense);
        }

        public async Task<Result<ExpenseDto>> Delete(Guid userId, Guid id, CancellationToken cancellationToken)
        {

            var user = await _userRepository.GetByIdAsync(userId, cancellationToken);

            var ex = await _repositoty.GetExpenseByUser(userId, id, cancellationToken);
            if (ex == null)
                return Result<ExpenseDto>.Failure("Нету такого расходы по Id");

            await _repositoty.DeleteAsync(ex);

            var expense =  new ExpenseDto
            {
                Id = ex.Id,
                UserId = ex.UserId,
                Description = ex.Description,
                Money = ex.Money,
                Category = ex.Category,
                Date = ex.Date
            };

            return Result<ExpenseDto>.Success(expense);

        }

        public async Task <Result<List<string>>> GetAllCategory()
        {
           var list =  await _repositoty.GetAllAvailableCategoriesAsync();
            if (list == null)
                return Result<List<string>>.Failure("Cписок категорий пуст");

            return Result<List<string>>.Success(list);
        }

        public async Task <Result<IEnumerable<ExpenseDto>>> GetAllExpensesAsync(Guid userId, CancellationToken cancellationToken)
        {

            var ex = await _repositoty.GetAllAsync(userId,cancellationToken);
            if (ex == null)
                return Result<IEnumerable<ExpenseDto>>.Failure("Список расходов пуст");


            var expense =  ex.Select(e => new ExpenseDto 
            { 
                Id = e.Id,
                UserId = e.UserId,
                Description = e.Description,
                Money = e.Money,
                Category = e.Category,
                Date = e.Date
           });

            return Result<IEnumerable<ExpenseDto>>.Success(expense);
            
        }

        
    }
}
