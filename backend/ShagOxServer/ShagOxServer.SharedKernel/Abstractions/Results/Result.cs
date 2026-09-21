using ShagOxServer.SharedKernel.Abstractions.Results.Enum;

namespace ShagOxServer.SharedKernel.Abstractions.Results;
public class Result<T>
{
    public bool IsSuccess { get; init; }

    public string? Error { get; init; }

    public T? Value { get; init; }

    public ResultErrorType? ErrorType { get; init; }


    public static Result<T> Success(T value)
        => new() {
            IsSuccess = true,
            Value = value
        };

    public static Result<T> Fail(
        string? error,
        ResultErrorType errorType = ResultErrorType.BadRequest)
         => new()
         {
             IsSuccess = false,
             Error = error ?? "Unknown error",
             ErrorType = errorType
         };

    public static Result<T> NotFound(string objectName)
        => new()
        {
            IsSuccess = false,
            Error = $"{objectName} not found",
            ErrorType = ResultErrorType.NotFound
        };

    public static Result<T> NotFound(Type type)
         => new()
         {
             IsSuccess = false,
             Error = $"{type.Name} not found",
             ErrorType = ResultErrorType.NotFound
         };

    public static Result<T> NotFound()
         => new()
         {
             IsSuccess = false,
             Error = $"{typeof(T).Name} not found",
             ErrorType = ResultErrorType.NotFound
         };

    public static Result<T> Unauthorized(
         string? error = null)
        => new ()
        {
            IsSuccess = false,
            Error = error ?? "Unauthorized",
            ErrorType = ResultErrorType.Unauthorized
        };

    public static Result<T> InternalServer(
        string? error = null)
       => new()
       {
           IsSuccess = false,
           Error = error ?? "Internal server error",
           ErrorType = ResultErrorType.InternalServer
       };

    public static Result<T> Forbidden()
         => new()
         {
             IsSuccess = false,
             Error = "Access denied",
             ErrorType = ResultErrorType.Forbidden
         };

    public static Result<T> AlreadyExists(string objectName)
       => new()
       {
           IsSuccess = false,
           Error = $"{objectName} already exists",
           ErrorType = ResultErrorType.Conflict
       };

    public static Result<T> AlreadyExists(Type type)
        => new()
        {
            IsSuccess = false,
            Error = $"{type.Name} already exists",
            ErrorType = ResultErrorType.Conflict
        };

    public static Result<T> AlreadyExists()
        => new()
        {
            IsSuccess = false,
            Error = $"{typeof(T).Name} already exists",
            ErrorType = ResultErrorType.Conflict
        };
}