using ExpenseTracker.Domain.Exceptions;
using ExpenseTracker.Domain.Value_Object;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public Email Email { get; private set; }
        public Password Password { get; private set; }


        private readonly List<Expense> _expenses = new();
        public IReadOnlyCollection<Expense> Expenses => _expenses.AsReadOnly();

        private User() { }

        public User(string name, Email email, Password password)
        {
            if (string.IsNullOrEmpty(name))
                throw new DomainException("Никнейм не может быть пустым!");

            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Password = password;
        }

        public bool IsPasswordValid(string InputPassword)
        {
            return Password.Verify(InputPassword);
        }


    }
}
