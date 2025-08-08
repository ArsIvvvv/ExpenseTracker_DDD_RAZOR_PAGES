using ExpenseTracker.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ExpenseTracker.Domain.Value_Object
{
    public class Password
    {
        private static readonly PasswordHasher<object> _hasher = new(); // класс для хеша пароля 
        public string HashValue { get; set; }

        private Password(string hashValue)
        {

             HashValue = hashValue;
        }

        public static Password Create(string plaintext) 
        { 
            if(string.IsNullOrWhiteSpace(plaintext))
                throw new DomainException("Пароль не может быть пустым");
            var hash = _hasher.HashPassword(null,plaintext); 
            return new Password(hash);

        }
        public bool Verify(string password)
        {
            var result = _hasher.VerifyHashedPassword(null, HashValue, password);
            return result == PasswordVerificationResult.Success;
        }

        public override string ToString()
        {
            return $"{HashValue}";
        }
    }
}
