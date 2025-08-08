using ExpenseTracker.Application.DTO;
using ExpenseTracker.Application.Interface;
using ExpenseTracker.Application.Service;
using ExpenseTracker.Domain.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Security.Claims;

namespace ExpenseTracker.Web.Pages.Expense
{
    [Authorize]
    public class ExpenseModel : PageModel
    {

        private readonly IExpenseService _ex;
        private readonly IUserService _userService;

        public ExpenseModel(IExpenseService ex, IUserService userService)
        {
            _ex = ex;
            _userService = userService;
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

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                ModelState.AddModelError(string.Empty, "¬ы не зарегестрированы!");
                return Page();
            }

            var userId = Guid.Parse(userIdClaim.Value);


            var result = await _userService.GetByIdAsync(userId, HttpContext.RequestAborted);
            if (!result.IsSuccess)
            {
                return RedirectToPage("/Account/Login");
            }

            var all = await _ex.GetAllExpensesAsync(result.Value!.Id, HttpContext.RequestAborted);
            if (!all.IsSuccess) 
            {
                ModelState.AddModelError(string.Empty, all.Error!);
                return Page();
            }

            Expenses = all.Value.ToList(); 

            if (!string.IsNullOrEmpty(CategoryName))
            {
                Expenses = all.Value.Where(p => p.Category.Name == CategoryName);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id, string? categoryName)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                ModelState.AddModelError(string.Empty, "¬ы не зарегестрированы!");
                return Page();
            }

            var userId = Guid.Parse(userIdClaim.Value);


            var result = await _userService.GetByIdAsync(userId, HttpContext.RequestAborted);
            if (!result.IsSuccess)
            {
                return RedirectToPage("/Account/Login");
            }

            var expense = await _ex.Delete(result.Value!.Id, id, HttpContext.RequestAborted);
            if (!expense.IsSuccess)
            {
                return Page();
            }
            return RedirectToPage(new { categoryName = categoryName }); // возвращает страницу с сортировкой
        }
    }
}
