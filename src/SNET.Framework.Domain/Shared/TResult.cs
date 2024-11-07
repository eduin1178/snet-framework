namespace SNET.Framework.Domain.Shared;

public class Result<T> : Result
{
    public T Data { get; private set; }

    private Result(T data, bool isSuccess, Error error)
        : base(isSuccess, error) =>
        Data = data;
    private Result(T data, bool isSuccess, string message)
        : base(isSuccess, message) =>
        Data = data;


    public static Result Success(T data, string message) => new Result<T>(data, true, message);

    public static Result Failure(T data, Error error) => new Result<T>(data, false, error);
}