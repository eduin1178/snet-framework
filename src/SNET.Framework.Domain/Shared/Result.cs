using System.Text;
using System.Text.Json.Serialization;

namespace SNET.Framework.Domain.Shared
{
    public class Result
    {
        [JsonConstructor]
        protected internal Result(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        protected internal Result(bool isSuccess, Error error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public bool IsSuccess { get;  }
        public string Message { get; }
        public Error Error { get; }
        public bool IsFailure => !IsSuccess;

        public static Result Success(string message) => new(true, message);

        public static Result<TValue> Success<TValue>(TValue value, string message) =>
            new(value, true, message);

        public static Result Failure(Error error) => new(false, error);

        public static Result<TValue> Failure<TValue>(Error error) =>
            new(default, false, error);

    }

    
}
