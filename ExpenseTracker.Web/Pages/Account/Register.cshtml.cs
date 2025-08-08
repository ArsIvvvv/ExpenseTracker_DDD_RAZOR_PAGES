using ExpenseTracker.Application.DTO.Command;
using ExpenseTracker.Application.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExpenseTracker.Web.Pages.Account
{
    public class RegisterModel : PageModel
    {

        private readonly IUserService _userService;

        public RegisterModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public UserInput Input { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var command = new RegisterUserCommand
            {
                Email = Input.Email,
                Name = Input.Name,
                Password = Input.Password
            };

            var result = await _userService.RegisterAsync(command, HttpContext.RequestAborted);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Error!);
                return Page();
            }

            return RedirectToPage("/Account/Login");
        }

    }

    public class UserInput
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
