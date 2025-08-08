using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application.DTO.Command
{
    public class LoginUserCommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
