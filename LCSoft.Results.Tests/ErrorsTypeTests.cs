namespace LCSoft.Results.Tests;

public class ErrorsTypeTests
{
    [Fact]
    public void TestError()
    {
        var error = TestErros.TestError;

        Assert.Equal(1, error.Code);
        Assert.Equal("TestError", error.Name);
    }

    [Fact]
    public void TestToString()
    {
        var error = TestErros.TestError;
        var name = error.ToString();
        var hashcode = error.GetHashCode();
        var code = TestErros.FromCode(1);

        Assert.Equal(code.Code, error.Code);
        Assert.Equal(hashcode, error.GetHashCode());
        Assert.Equal(name, error.Name);
        Assert.True(code.Equals(error));
    }


    [Fact]
    public void TestErrors()
    {
        var error = TestErros.TestError;
        var code = TestErros.FromCode(2);

        Assert.NotEqual(code.Code, error.Code);
        Assert.False(code.Equals(error));
    }

    [Fact]
    public void TestErrorsNull()
    {
        var error = TestErros.TestError;
        var code = TestErros.FromCode(3);

        var error2 = TestErros.FromCode(2);
        var results = error2.Equals(error);
        Assert.False(results);
    }

    [Fact]
    public void TestErrorsNotSabeType()
    {
        var error = TestErros.TestError;
        var error2 = Error.GenericFailure("GE-01", "Generic");

        var results = error.Equals(error2);
        Assert.False(results);
    }

    [Fact]
    public void FromCode_ReturnsErrorOrNull_CoverBranches()
    {
        // Arrange
        var existingError = new SampleError(1, "ErrorOne");
        var nonExistingCode = 999;

        // Act
        var foundError = ErrorsType.FromCode(existingError.Code);
        var notFoundError = ErrorsType.FromCode(nonExistingCode);

        // Assert
        Assert.NotNull(foundError);
        Assert.Equal(existingError.Code, foundError.Code);

        Assert.Null(notFoundError);
    }
}

public class TestErros : ErrorsType
{
    public static readonly TestErros TestError = new(1, "TestError");
    public static readonly TestErros TestError2 = new(2, "TestError2");
    private TestErros(int code, string name) : base(code, name)
    {
        Register(this);
    }
}

public class SampleError : ErrorsType
{
    public SampleError(int code, string name) : base(code, name)
    {
        Register(this);
    }
}
