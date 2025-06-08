namespace LC.Results;

public sealed record Results : IResult
{
    public bool IsSuccess { get; } = false;
    public bool IsFailure => !IsSuccess;
    public ErrorsType? Error { get; }

    private Results() {
        IsSuccess = true;
        Error = default;
    }

    private Results(ErrorsType error)
    {
        IsSuccess = false;
        Error = error;
    }

    public static Results Success() => new();
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

public sealed record Results<TValue> : IResult
{
    // If you want theses objects be accessible only on math()
    // make them private otherwise let them public
    // but will make things more complex, and work with delegates 
    // maybe you want these values accessible or create method for that
    public readonly TValue? Value;
    public readonly ErrorsType? Error;

    public bool IsSuccess { get; } = false;
    public bool IsError => !IsSuccess;

    private Results() { }

    private Results(TValue value)
    {
        IsSuccess = true;
        Value = value;
        Error = default;
    }

    private Results(ErrorsType error)
    {
        IsSuccess = false;
        Value = default;
        Error = error;
    }

    public static implicit operator Results<TValue>(TValue value) => new(value);

    public static implicit operator Results<TValue>(ErrorsType error) => new(error);

    public TResult Match<TResult>(Func<TValue, TResult> onSuccess, Func<ErrorsType, TResult> onFailure)
                => IsSuccess ? onSuccess(Value!) : onFailure(Error!);

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

    public static Results<TValue> Failure(ErrorsType error) => new(error);
    public static Results<TValue> Success(TValue value) => new(value);
}
