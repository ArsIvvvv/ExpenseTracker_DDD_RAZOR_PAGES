using ExpenseTracker.Domain.Value_Object;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Domain.Entities
{
    public class Expense
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Description { get; private set; } = string.Empty;
        public Money Money { get; private set; }
        public Category Category { get; private set; }
        public DateTime Date { get; private set; }

        public User User { get; private set; }

        private Expense() { }

        public Expense(User user,string description, Money money, Category category, DateTime date)
        {
            Id = Guid.NewGuid();
            UserId = user.Id;
            Description = description;
            Money = money;
            Category = category;
            Date = date;
        }
    }
}
