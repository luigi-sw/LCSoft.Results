namespace LCSoft.Results.Tests;

public class IResultsTests
{
    [Theory]
    [InlineData(true, false)]
    [InlineData(false, true)]
    public void IsFailure_ReturnsInverseOfIsSuccess(bool isSuccess, bool expectedIsFailure)
    {
        IResult result = new DummyResult(isSuccess);

        // Access IsFailure via interface reference to hit default implementation
        bool actualIsFailure = result.IsFailure;

        Assert.Equal(expectedIsFailure, actualIsFailure);
    }
}

public class DummyResult : IResult
{
    public bool IsSuccess { get; }

    public DummyResult(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }
}