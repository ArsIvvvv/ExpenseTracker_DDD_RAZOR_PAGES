using ExpenseTracker.Application.DTO.Command;
using ExpenseTracker.Application.Interface;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Value_Object;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ExpenseTracker.Web.Pages.Expense
{
    [Authorize]
    public class CreateExpenseModel : PageModel
    {
        private readonly IExpenseService _ex;
        private readonly IUserService _userService;

        public List<string> CategoryList { get; set; } = new();

        public CreateExpenseModel(IExpenseService ex, IUserService userService)
        {
           _ex = ex;      
           _userService = userService;
        }

        [BindProperty]
        public  ExpenseInput Input { get; set; } = new();

        public async Task <IActionResult> OnGetAsync() {

            var expense =  await _ex.GetAllCategory();
            if (!expense.IsSuccess) 
            { 
                ModelState.AddModelError(string.Empty, expense.Error);
                return Page();  
            }
            CategoryList = expense.Value;
            return Page();
        }   

        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                var expense = await _ex.GetAllCategory();
                if (!expense.IsSuccess)
                {
                    ModelState.AddModelError(string.Empty, expense.Error);
                    return Page();
                }
                CategoryList = expense.Value;
                return Page();
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                ModelState.AddModelError(string.Empty, "Вы не зарегестрированы!");
                return Page();
            }

            var userId = Guid.Parse(userIdClaim.Value);


            var result = await _userService.GetByIdAsync(userId, HttpContext.RequestAborted);
            if (!result.IsSuccess)
            {
                return RedirectToPage("/Account/Login");
            }

            var command = new CreateExpenseCommand
            {
                UserId = userId,
                Description = Input.Description,
                Amount = Input.Amount,
                Category = Input.Category,
                Date = Input.Date

            };

            await _ex.AddExpenseAsync(command, HttpContext.RequestAborted);
            TempData["SuccessMessage"] = "Расход успешно добавлен";

            return RedirectToPage("/Expense/Expense");
        }
    }

    public class ExpenseInput
    {
        [Required(ErrorMessage = "Нужно обязательно заполнить поле 'Описание'")]
        [MaxLength(50)]
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Нужно обязательно заполнить поле 'Сумма'")]
        [Range(0.01, 100000000.00, ErrorMessage = "Сумма должна быть в диапазоне от 1 до 99999999")]
        public decimal Amount { get; set; } = 0;
        public string Category { get; set; }

        [Required(ErrorMessage = "Нужно обязательно заполнить поле 'Дата'")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }

}
