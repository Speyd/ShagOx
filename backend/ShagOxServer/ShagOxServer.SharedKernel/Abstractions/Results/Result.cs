using ShagOxServer.SharedKernel.Abstractions.Resources.Results;
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
             Error = error ?? ResultResources.UnknownError,
             ErrorType = errorType
         };

    public static Result<T> NotFound(string objectName)
        => new()
        {
            IsSuccess = false,
            Error = string.Format(ResultResources.NotFound, objectName),
            ErrorType = ResultErrorType.NotFound
        };


    public static Result<T> Unauthorized(
         string? error = null)
        => new ()
        {
            IsSuccess = false,
            Error = error ?? ResultResources.Unauthorized,
            ErrorType = ResultErrorType.Unauthorized
        };

    public static Result<T> InternalServer(
        string? error = null)
       => new()
       {
           IsSuccess = false,
           Error = error ?? ResultResources.InternalServerError,
           ErrorType = ResultErrorType.InternalServer
       };

    public static Result<T> Forbidden()
         => new()
         {
             IsSuccess = false,
             Error = ResultResources.AccessDenied,
             ErrorType = ResultErrorType.Forbidden
         };

    public static Result<T> AlreadyExists(string objectName)
       => new()
       {
           IsSuccess = false,
           Error = string.Format(ResultResources.AlreadyExists, objectName),
           ErrorType = ResultErrorType.Conflict
       };
}