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
        public string Description { get; private set; } = string.Empty;
        public Money Money { get; private set; }
        public Category Category { get; private set; }
        public DateTime Date { get; private set; }

        private Expense() { }

        public Expense( string description, Money money, Category category, DateTime date)
        {
            Id = Guid.NewGuid();
            Description = description;
            Money = money;
            Category = category;
            Date = date;
        }
    }
}
