using ExpenseTracker.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ExpenseTracker.Domain.Value_Object
{
    public class Email
    {
         public string Value { get;}

        public Email(string value)
        {
            if(string.IsNullOrEmpty(value) || !value.Contains('@'))
            {
                throw new DomainException("Неверный формат почты");
            }

            Value = value;
        }

        public override string ToString()
        {
            return $"{Value}";
        }

        public override bool Equals(object obj) =>
        obj is Email other && Value == other.Value;
    }
}
