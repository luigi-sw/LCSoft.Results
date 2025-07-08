#if NET5_0
using System;
#endif
using System.Diagnostics.CodeAnalysis;

namespace LCSoft.Results
{
    public sealed record sResults : IResult<Error>
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

        [ExcludeFromCodeCoverage]
        public static sResults Success() => new();
        [ExcludeFromCodeCoverage]
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

    public sealed record sResults<TValue> : IResult<Error>
    {
        // If you want theses objects be accessible only on math()
        // make them private otherwise let them public
        // but will make things more complex, and work with delegates 
        // maybe you want these values accessible or create method for that
        public TValue? Value { get; }
        public Error? Error { get; }

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

        TResult IResult<Error>.Match<TResult>(Func<TResult> onSuccess, Func<Error, TResult> onFailure)
           => IsSuccess ? onSuccess() : onFailure(Error!);

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

        void IResult<Error>.Match(Action? success, Action<Error>? failure)
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
        public static sResults<TValue> Failure(Error error) => new(error);
        [ExcludeFromCodeCoverage]
        public static sResults<TValue> Success(TValue value) => new(value);
    }
}
