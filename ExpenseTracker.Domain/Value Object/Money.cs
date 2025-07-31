using ExpenseTracker.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ExpenseTracker.Domain.Value_Object
{
    public class Money
    {
        public decimal Amount { get; private set; }

        public string Currency { get; private set; } = "RUB";
        public Money(decimal amount) 
        { 
            if(amount == 0)
                throw new DomainException("Сумма расхода обязательно");    

            Amount = amount;
        }

        public override string ToString()
        {
            return $"{Amount} {Currency}";
        }

        public override bool Equals(object obj) =>
        obj is Money other && Amount == other.Amount;

    }
}
