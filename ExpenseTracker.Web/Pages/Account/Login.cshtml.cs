using ExpenseTracker.Application.DTO.Command;
using ExpenseTracker.Application.Interface;
using ExpenseTracker.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace ExpenseTracker.Web.Pages.Account
{
    public class LoginModel : PageModel
    {

        private readonly IUserService _userService;

        public LoginModel(IUserService userService)
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
            var command = new LoginUserCommand
            {
                Email = Input.Email,
                Password = Input.Password
            };

            var result = await _userService.LoginAsync(command, HttpContext.RequestAborted);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Error);
                return Page();
            }

            var claim = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, result.Value!.Id.ToString()),
                new Claim(ClaimTypes.Name, result.Value.Name),
                new Claim(ClaimTypes.Email, result.Value.Email.Value)
            };

            var claimIdentity = new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
               new ClaimsPrincipal(claimIdentity));

            return RedirectToPage("/Index");
        }


        public class UserInput
        {
            [Required(ErrorMessage = "Почта обязательна")]
            [EmailAddress(ErrorMessage = "Неверный формат почты")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Пароль обязательно")]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }
    }
}
