namespace LCSoft.Results;

public sealed record sResults : IResult
{
    public bool IsSuccess { get; } = false;
    public bool IsFailure => !IsSuccess;
    public Error? Error { get; }

    private sResults()
    {
        IsSuccess = true;
        Error = default;
    }

    private sResults(Error error)
    {
        IsSuccess = false;
        Error = error;
    }

    public static sResults Success() => new();
    public static sResults Failure(Error error) => new(error);

    public TResult Match<TResult>(Func<TResult> onSuccess, Func<Error, TResult> onFailure)
                => IsSuccess ? onSuccess() : onFailure(Error!);

    public void Match(Action? success = null, Action<Error>? failure = null)
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

public sealed record sResults<TValue> : IResult
{
    // If you want theses objects be accessible only on math()
    // make them private otherwise let them public
    // but will make things more complex, and work with delegates 
    // maybe you want these values accessible or create method for that
    public readonly TValue? Value;
    public readonly Error? Error;

    public bool IsSuccess { get; } = false;
    public bool IsError => !IsSuccess;

    private sResults() { }

    private sResults(TValue value)
    {
        IsSuccess = true;
        Value = value;
        Error = default;
    }

    private sResults(Error error)
    {
        IsSuccess = false;
        Value = default;
        Error = error;
    }

    public static implicit operator sResults<TValue>(TValue value) => new(value);

    public static implicit operator sResults<TValue>(Error error) => new(error);

    public TResult Match<TResult>(Func<TValue, TResult> onSuccess, Func<Error, TResult> onFailure)
                => IsSuccess ? onSuccess(Value!) : onFailure(Error!);

    public void Match(Action<TValue>? success = null, Action<Error>? failure = null)
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

    public static sResults<TValue> Failure(Error error) => new(error);
    public static sResults<TValue> Success(TValue value) => new(value);
}
