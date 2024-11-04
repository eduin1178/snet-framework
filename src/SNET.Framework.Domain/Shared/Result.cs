namespace SNET.Framework.Domain.Shared;

public class Result
{
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
    public bool IsSuccess { get; }
    public string Message { get; }

    public bool IsFailure => !IsSuccess;

    public Error Error { get; }

    public static Result Success(string message) => new(true, message);

    public static Result Failure(Error error) => new(false, error);


}



