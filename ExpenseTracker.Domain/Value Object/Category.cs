using ExpenseTracker.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Domain.Value_Object
{
    public class Category
    {
        public string Name { get; private set; }

        public Category(string name)
        {
            if (name == null) throw new DomainException("Категория обязательна");

            Name = name;
        }
        public override string ToString()
        {
            return $"{Name}";
        }

        public override bool Equals(object obj) =>
        obj is Category other && Name == other.Name;
    }
}
