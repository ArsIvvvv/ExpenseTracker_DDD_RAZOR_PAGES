using ExpenseTracker.Application.DTO;
using ExpenseTracker.Application.Interface;
using ExpenseTracker.Domain.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace ExpenseTracker.Web.Pages.Expense
{
    public class ExpenseModel : PageModel
    {

        private readonly IExpenseService _ex;


        public ExpenseModel(IExpenseService ex)
        {
            _ex = ex;
        }

        [BindProperty(SupportsGet =true)]
        public string? CategoryName {  get; set; }= string.Empty; 

        public List<string> CategoryList { get; set; } = new();

        public IEnumerable<ExpenseDto> Expenses { get; set; } = new List<ExpenseDto>();


        public async Task<IActionResult> OnGetAsync()
        {
            var expense = await _ex.GetAllCategory();
            if (!expense.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, expense.Error);
                return Page();
            }

            CategoryList = expense.Value;

            var all = await _ex.GetAllExpensesAsync();

            Expenses = all.Value.ToList(); 

            if (!string.IsNullOrEmpty(CategoryName))
            {
                Expenses = all.Value.Where(p => p.Category.Name == CategoryName);
            }

            return Page();
        }


        public async Task<IActionResult> OnPostDeleteAsync(Guid id, string? categoryName)
        {     
            await _ex.Delete(id);
            return RedirectToPage(new { categoryName = categoryName }); // возвращает страницу с сортировкой
        }
    }
}
