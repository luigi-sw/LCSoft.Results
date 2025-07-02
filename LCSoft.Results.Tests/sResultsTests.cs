
using System.Reflection;

namespace LCSoft.Results.Tests;

public class sResultsTests
{
    [Fact]
    public void Bool_Result_Success()
    {
        var results = sResults.Success();

        results.Match(
            success: () => Console.WriteLine("Success")
        );

        var resultado = results.Match(
            onSuccess: () => "Success",
            onFailure: error => $"Failure: {error}"
        );

        Assert.Equal("Success", resultado);
        Assert.True(results.IsSuccess);
        Assert.False(results.IsFailure);
    }

    [Fact]
    public void Bool_Result_Failure()
    {
        var results = sResults.Failure(Error.GenericFailure("GE-01", "GenericFailure"));

        results.Match(
            //success: () => Console.WriteLine("Success"),
            failure: error => Console.WriteLine($"Failure: {error}")
        );

        var resultado = results.Match(
            onSuccess: () => "Success",
            onFailure: error => $"Failure: {error.Message}"
        );

        Assert.Equal("Failure: GenericFailure", resultado);
        Assert.False(results.IsSuccess);
        Assert.True(results.IsFailure);
    }

    [Fact]
    public void MatchBool_SuccessWithNullAction_DoesNotThrow()
    {
        var result = sResults.Success();

        var exception = Record.Exception(() => result.Match());

        Assert.Null(exception); // means no delegate passed and no error thrown
    }

    [Fact]
    public void MatchBool_FailureWithNullAction_DoesNotThrow()
    {
        var error = Error.GenericFailure("GE-01", "GenericFailure");
        sResults result = sResults.Failure(error);

        var exception = Record.Exception(() => result.Match());

        Assert.Null(exception);
    }



    [Fact]
    public void Typed_Result_Success()
    {
        var results = sResults<string>.Success("Success");

        results.Match(
            success: value => Console.WriteLine(value)
        );

        var resultado = results.Match(
            onSuccess: value => "Success",
            onFailure: error => $"Failure: {error}"
        );

        Assert.Equal("Success", resultado);
        Assert.True(results.IsSuccess);
        Assert.False(results.IsError);
    }

    [Fact]
    public void Typed_Result_Failure()
    {
        var results = sResults<string>.Failure(Error.GenericFailure("GE-01", "GenericFailure"));

        results.Match(
            failure: error => Console.WriteLine($"Failure: {error}")
        );

        var resultado = results.Match(
            onSuccess: value => "Success",
            onFailure: error => $"Failure: {error.Message}"
        );

        Assert.Equal("Failure: GenericFailure", resultado);
        Assert.False(results.IsSuccess);
        Assert.True(results.IsError);
    }

    [Fact]
    public void ImplicitConversion_FromValue_SetsSuccess()
    {
        // Arrange
        int value = 42;

        // Act
        sResults<int> result = value;

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(42, result.Value);
        Assert.Null(result.Error);
    }

    [Fact]
    public void ImplicitConversion_FromError_SetsFailure()
    {
        // Arrange
        var error = Error.GenericFailure("GE-01", "GenericFailure");

        // Act
        sResults<int> result = error;

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(error, result.Error);
    }

    [Fact]
    public void PrivateConstructor_CanBeInvoked()
    {
        // Arrange
        var type = typeof(sResults<int>);

        // Act
        var ctor = type.GetConstructor(
            BindingFlags.Instance | BindingFlags.NonPublic,
            binder: null,
            types: Type.EmptyTypes,
            modifiers: null);

        Assert.NotNull(ctor); // Constructor exists

        var instance = ctor.Invoke(null);

        // Assert
        Assert.NotNull(instance);
        Assert.IsType<sResults<int>>(instance);
    }

    [Fact]
    public void Match_SuccessWithNullAction_DoesNotThrow()
    {
        var result = sResults<int>.Success(42);

        var exception = Record.Exception(() => result.Match());

        Assert.Null(exception); // means no delegate passed and no error thrown
    }

    [Fact]
    public void Match_FailureWithNullAction_DoesNotThrow()
    {
        var error = Error.GenericFailure("GE-01", "GenericFailure");
        sResults<int> result = error;

        var exception = Record.Exception(() => result.Match());

        Assert.Null(exception);
    }

    [Fact]
    public void Match_Func_ExplicitInterface_Success_CallsOnSuccess()
    {
        var result = sResults<int>.Success(42);
        IResult<Error> iResult = result;

        bool successCalled = false;
        bool failureCalled = false;

        var ret = iResult.Match(
            onSuccess: () => { successCalled = true; return 1; },
            onFailure: err => { failureCalled = true; return -1; });

        Assert.True(successCalled);
        Assert.False(failureCalled);
        Assert.Equal(1, ret);
    }

    [Fact]
    public void Match_Func_ExplicitInterface_Failure_CallsOnFailure()
    {
        var error = Error.GenericFailure("42", "fail");
        var result = sResults<int>.Failure(error);
        IResult<Error> iResult = result;

        bool successCalled = false;
        bool failureCalled = false;

        var ret = iResult.Match(
            onSuccess: () => { successCalled = true; return 1; },
            onFailure: err => { failureCalled = true; return int.Parse(err.Code); }); // Fixed: Ensure the return type matches the delegate signature

        Assert.False(successCalled);
        Assert.True(failureCalled);
        Assert.Equal(int.Parse(error.Code), ret); // Fixed: Ensure the return type matches the expected value
    }

    // --- ACTION-based Match ---

    [Fact]
    public void Match_Action_ExplicitInterface_Success_CallsOnSuccess()
    {
        var result = sResults<int>.Success(42);
        IResult<Error> iResult = result;

        bool successCalled = false;
        bool failureCalled = false;

        iResult.Match(
            () => { successCalled = true; },
            err => { failureCalled = true; });

        Assert.True(successCalled);
        Assert.False(failureCalled);
    }

    [Fact]
    public void Match_Action_ExplicitInterface_Failure_CallsOnFailure()
    {
        var error = Error.GenericFailure("42", "fail");
        var result = sResults<int>.Failure(error);
        IResult<Error> iResult = result;

        bool successCalled = false;
        bool failureCalled = false;

        iResult.Match(
            () => { successCalled = true; },
            err => { failureCalled = true; });

        Assert.False(successCalled);
        Assert.True(failureCalled);
    }

    // --- Null delegate coverage for Action Match ---

    [Fact]
    public void Match_Action_ExplicitInterface_Success_WithNullDelegates_DoesNotThrow()
    {
        var result = sResults<int>.Success(42);
        IResult<Error> iResult = result;

        var ex = Record.Exception(() => iResult.Match(null, null));
        Assert.Null(ex);
    }

    [Fact]
    public void Match_Action_ExplicitInterface_Failure_WithNullDelegates_DoesNotThrow()
    {
        var error = Error.GenericFailure("42", "fail");
        var result = sResults<int>.Failure(error);
        IResult<Error> iResult = result;

        var ex = Record.Exception(() => iResult.Match(null, null));
        Assert.Null(ex);
    }

}
