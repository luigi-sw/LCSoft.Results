namespace LCSoft.Results;


public interface IResult
{
    bool IsSuccess { get; }
    bool IsFailure => !IsSuccess;
}

public interface IResult<out TError> : IResult
{
    TError? Error { get; }

    TResult Match<TResult>(Func<TResult> onSuccess, Func<TError, TResult> onFailure);
    void Match(Action? onSuccess = null, Action<TError>? onFailure = null);
}

public interface IResult<TValue, TError> : IResult<TError>
{
    TResult Match<TResult>(Func<TValue, TResult> onSuccess, Func<TError, TResult> onFailure);
    void Match(Action<TValue>? onSuccess = null, Action<TError>? onFailure = null);
}