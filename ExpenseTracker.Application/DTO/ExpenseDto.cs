using ExpenseTracker.Domain.Value_Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application.DTO
{
    public class ExpenseDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Description { get; set; } = string.Empty;
        public Money Money {  get; set; }
        public Category Category {  get; set; }
        public DateTime Date {  get; set; }
    }
}
