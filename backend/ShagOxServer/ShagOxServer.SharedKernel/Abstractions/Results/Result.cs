namespace ShagOxServer.SharedKernel.Abstractions.Results;
public class Result<T>
{
    public bool IsSuccess { get; init; }

    public string? Error { get; init; }

    public T? Value { get; init; }


    public static Result<T> Success(T value)
        => new() { IsSuccess = true, Value = value };

    public static Result<T> Fail(string error)
        => new() { IsSuccess = false, Error = error };

    public static Result<T> NotFound(string objectName)
        => new() { IsSuccess = false, Error =  $"{objectName} not found" };

    public static Result<T> Unauthorized()
        => new() { IsSuccess = false, Error = "Unauthorized" };

    public static Result<T> Forbidden()
        => new() { IsSuccess = false,  Error = "Access denied" };

    public static Result<T> AlreadyExists(string objectName)
        => new() { IsSuccess = false, Error = $"{objectName} already exists" };
}