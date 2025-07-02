using System.Diagnostics.CodeAnalysis;

namespace LCSoft.Results;

public sealed record Results : IResult<ErrorsType>
{
    public bool IsSuccess { get; } = false;
    public bool IsFailure => !IsSuccess;
    public ErrorsType? Error { get; }

    internal Results() {
        IsSuccess = true;
        Error = default;
    }

    internal Results(ErrorsType error)
    {
        IsSuccess = false;
        Error = error;
    }

    [ExcludeFromCodeCoverage]
    public static Results Success() => new();
    [ExcludeFromCodeCoverage]
    public static Results Failure(ErrorsType error) => new(error);

    public TResult Match<TResult>(Func<TResult> onSuccess, Func<ErrorsType, TResult> onFailure)
                => IsSuccess ? onSuccess() : onFailure(Error!);

    public void Match(Action? success = null, Action<ErrorsType>? failure = null)
    {
        if (IsSuccess)
        {
            success?.Invoke();
        }
        else
        {
            failure?.Invoke(Error!);
        }
    }
}

public sealed record Results<TValue> : IResult<TValue, ErrorsType>
{
    // If you want theses objects be accessible only on math()
    // make them private otherwise let them public
    // but will make things more complex, and work with delegates 
    // maybe you want these values accessible or create method for that
    public TValue? Value { get; }
    public ErrorsType? Error { get; }

    public bool IsSuccess { get; } = false;
    public bool IsError => !IsSuccess;
    //ErrorsType? IResult<ErrorsType>.Error => throw new NotImplementedException();

    internal Results() { }

    internal Results(TValue value)
    {
        IsSuccess = true;
        Value = value;
        Error = default;
    }

    internal Results(ErrorsType error)
    {
        IsSuccess = false;
        Value = default;
        Error = error;
    }

    public static implicit operator Results<TValue>(TValue value) => new(value);

    public static implicit operator Results<TValue>(ErrorsType error) => new(error);

    public TResult Match<TResult>(Func<TValue, TResult> onSuccess, Func<ErrorsType, TResult> onFailure)
                => IsSuccess ? onSuccess(Value!) : onFailure(Error!);

    TResult IResult<ErrorsType>.Match<TResult>(
    Func<TResult> onSuccess,
    Func<ErrorsType, TResult> onFailure)
    => IsSuccess ? onSuccess() : onFailure(Error!);

    public void Match(Action<TValue>? success = null, Action<ErrorsType>? failure = null)
    {
        if (IsSuccess)
        {
            success?.Invoke(Value!);
        }
        else
        {
            failure?.Invoke(Error!);
        }
    }

    void IResult<ErrorsType>.Match(Action? success, Action<ErrorsType>? failure)
    {
        if (IsSuccess)
        {
            success?.Invoke();
        }
        else
        {
            failure?.Invoke(Error!);
        }
    }

    [ExcludeFromCodeCoverage]
    public static Results<TValue> Failure(ErrorsType error) => new(error);
    [ExcludeFromCodeCoverage]
    public static Results<TValue> Success(TValue value) => new(value);
}
