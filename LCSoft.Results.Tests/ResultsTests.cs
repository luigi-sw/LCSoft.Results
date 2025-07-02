
using System.Reflection;

namespace LCSoft.Results.Tests;

public class ResultsTests
{
    [Fact]
    public void Bool_Result_Success()
    {
        var results = Results.Success();

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
        var results = Results.Failure(StandardErrorType.GenericFailure);

        results.Match(
            //success: () => Console.WriteLine("Success"),
            failure: error => Console.WriteLine($"Failure: {error}")
        );

        var resultado = results.Match(
            onSuccess: () => "Success",
            onFailure: error => $"Failure: {error}"
        );

        Assert.Equal("Failure: GenericFailure", resultado);
        Assert.False(results.IsSuccess);
        Assert.True(results.IsFailure);
    }

    [Fact]
    public void MatchBool_SuccessWithNullAction_DoesNotThrow()
    {
        var result = Results.Success();

        var exception = Record.Exception(() => result.Match());

        Assert.Null(exception); // means no delegate passed and no error thrown
    }

    [Fact]
    public void MatchBool_FailureWithNullAction_DoesNotThrow()
    {
        var error = StandardErrorType.GenericFailure;
        Results result = Results.Failure(error);

        var exception = Record.Exception(() => result.Match());

        Assert.Null(exception);
    }



    [Fact]
    public void Typed_Result_Success()
    {
        var results = Results<string>.Success("Success");

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
        var results = Results<string>.Failure(StandardErrorType.GenericFailure);

        results.Match(
            failure: error => Console.WriteLine($"Failure: {error}")
        );

        var resultado = results.Match(
            onSuccess: value => "Success",
            onFailure: error => $"Failure: {error}"
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
        Results<int> result = value;

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(42, result.Value);
        Assert.Null(result.Error);
    }

    [Fact]
    public void ImplicitConversion_FromError_SetsFailure()
    {
        // Arrange
        var error = StandardErrorType.GenericFailure;

        // Act
        Results<int> result = error;

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(error, result.Error);
    }

    [Fact]
    public void PrivateConstructor_CanBeInvoked()
    {
        // Arrange
        var type = typeof(Results<int>);

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
        Assert.IsType<Results<int>>(instance);
    }

    [Fact]
    public void Match_SuccessWithNullAction_DoesNotThrow()
    {
        var result = Results<int>.Success(42);

        var exception = Record.Exception(() => result.Match());

        Assert.Null(exception); // means no delegate passed and no error thrown
    }

    [Fact]
    public void Match_FailureWithNullAction_DoesNotThrow()
    {
        var error = StandardErrorType.GenericFailure;
        Results<int> result = error;

        var exception = Record.Exception(() => result.Match());

        Assert.Null(exception);
    }

    [Fact]
    public void Match_Func_ExplicitInterface_Success_CallsOnSuccess()
    {
        // Arrange
        var result = new Results<int>(42);
        IResult<ErrorsType> iResult = result;

        bool successCalled = false;
        bool failureCalled = false;

        // Act
        var ret = iResult.Match(
            onSuccess: () => { successCalled = true; return 100; },
            onFailure: err => { failureCalled = true; return -1; });

        // Assert
        Assert.True(successCalled);
        Assert.False(failureCalled);
        Assert.Equal(100, ret);
    }

    [Fact]
    public void Match_Func_ExplicitInterface_Failure_CallsOnFailure()
    {
        // Arrange
        var error = StandardErrorType.GenericFailure;
        var result = new Results<int>(error);
        IResult<ErrorsType> iResult = result;

        bool successCalled = false;
        bool failureCalled = false;

        // Act
        var ret = iResult.Match(
            onSuccess: () => { successCalled = true; return 100; },
            onFailure: err => { failureCalled = true; return err.Code; });

        // Assert
        Assert.False(successCalled);
        Assert.True(failureCalled);
        Assert.Equal(error.Code, ret);
    }

    [Fact]
    public void Match_Action_ExplicitInterface_Success_CallsOnSuccess()
    {
        // Arrange
        var result = new Results<int>(42);
        IResult<ErrorsType> iResult = result;

        bool successCalled = false;
        bool failureCalled = false;

        // Act
        iResult.Match(
        () => { successCalled = true; },
        err => { failureCalled = true; });

        // Assert
        Assert.True(successCalled);
        Assert.False(failureCalled);
    }

    [Fact]
    public void Match_Action_ExplicitInterface_Failure_CallsOnFailure()
    {
        // Arrange
        var error = StandardErrorType.GenericFailure;
        var result = new Results<int>(error);
        IResult<ErrorsType> iResult = result;

        bool successCalled = false;
        bool failureCalled = false;

        // Act
        iResult.Match(
        () => { successCalled = true; },
        err => { failureCalled = true; });

        // Assert
        Assert.False(successCalled);
        Assert.True(failureCalled);
    }

    [Fact]
    public void Match_Action_ExplicitInterface_Success_WithNullDelegates_DoesNotThrow()
    {
        var result = new Results<int>(42);
        IResult<ErrorsType> iResult = result;

        // Act & Assert: no exceptions when delegates are null
        var ex = Record.Exception(() => iResult.Match(null, null));

        Assert.Null(ex);
    }

    [Fact]
    public void Match_Action_ExplicitInterface_Failure_WithNullDelegates_DoesNotThrow()
    {
        var error = StandardErrorType.GenericFailure;
        var result = new Results<int>(error);
        IResult<ErrorsType> iResult = result;

        var ex = Record.Exception(() => iResult.Match(null, null));

        Assert.Null(ex);
    }
}
