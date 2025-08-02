using ExpenseTracker.Domain.Value_Object;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application.DTO.Command
{
    public class CreateExpenseCommand
    { 
            public string Description { get; set; } = string.Empty;
            public decimal Amount { get; set; }
            public string Category { get; set; } = string.Empty;
            public DateTime Date { get; set; }
    }
}
