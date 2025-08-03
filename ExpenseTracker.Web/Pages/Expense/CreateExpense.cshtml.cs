using ExpenseTracker.Application.DTO.Command;
using ExpenseTracker.Application.Interface;
using ExpenseTracker.Domain.Value_Object;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ExpenseTracker.Web.Pages.Expense
{
    public class CreateExpenseModel : PageModel
    {
        private readonly IExpenseService _ex;

        public List<string> CategoryList { get; set; } = new();

        public CreateExpenseModel(IExpenseService ex)
        {
            _ex = ex;      
        }

        [BindProperty]
        public  ExpenseInput Input { get; set; } = new();

        public async Task <IActionResult> OnGetAsync() {

            var expense =  await _ex.GetAllCategory(HttpContext.RequestAborted);
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
                var expense = await _ex.GetAllCategory(HttpContext.RequestAborted);
                if (!expense.IsSuccess)
                {
                    ModelState.AddModelError(string.Empty, expense.Error);
                    return Page();
                }
                CategoryList = expense.Value;
                return Page();
            }

            var command = new CreateExpenseCommand
            {
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
