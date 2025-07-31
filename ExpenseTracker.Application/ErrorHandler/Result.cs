using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application.ErrorHandler
{
    public class Result<T>
    {

        public bool IsSuccess {  get;  }
        public string? Error { get; } = string.Empty;
        public T? Value { get; }

        private Result(T? value, bool isSuccess, string error)
        {
            IsSuccess = isSuccess;
            Error = error;
            Value = value;
        }

        public static Result<T> Success(T? value) => new(value, true, null);

        public static Result<T> Failure(string? error) => new(default, false, error);
    }
}
