using LCSoft.Results;


#region Results<T> Usage
#region Basic Success Usage
Console.WriteLine("Testing Results<T> with sucess.");

Results<int> results = ParseNumber("2");

Console.WriteLine("Result = {0}", results.Value);

#region Results with Match() usage
Console.WriteLine("Testing Results<T> with sucess with Match()");
var Value = results.Match(
    onSuccess: value => value,
    onFailure: error => 
    {
        Console.WriteLine("Error = {0}", error);
        return 0;
    }
);
Console.WriteLine("Value = {0}", Value);
#endregion

#region Result with Match() without returned value
Console.WriteLine("If you want Match() without a returned value");
results.Match(value => 
    { 
        Console.WriteLine("Result = {0}", value); 
    },
    error => {
        Console.WriteLine("Error = {0}", error);
    });
Console.WriteLine("Testing Results<T> with failure.");
#endregion
#endregion

#region Basic Failure Usage
results = ParseNumber("x");

Console.WriteLine("Result = {0}", results.Error);

#region Results with Match() usage
Console.WriteLine("Testing Results<T> with failure with Match()");
Value = results.Match(
    onSuccess: value => value,
    onFailure: error =>
    {
        Console.WriteLine("Error = {0}", error);
        return 0;
    }
);
Console.WriteLine("Value = {0}", Value);
#endregion
#region Result with Match() without returned value
Console.WriteLine("If you want Match() without a returned value");

results.Match(value =>
    {
        Console.WriteLine("Result = {0}", value);
    },
    error => {
        Console.WriteLine("Error = {0}", error);
    });
#endregion
#endregion

#region Manual Result Testing Usage
Console.WriteLine("You also can use manually");
if (results.IsSuccess)
{
    Console.WriteLine("Success");
}
else
{
    Console.WriteLine("Failure");
}
#endregion

#region Example Functions
static Results<int> ParseNumber(string input)
{
    if (int.TryParse(input, out int number))
        return Results<int>.Success(number);
    return Results<int>.Failure(StandardErrorType.GenericFailure);
}
#endregion
#endregion


#region sResults<T> Usage

#region Basic Success Usage
Console.WriteLine("Testing sResults<T> with sucess.");

sResults<int> sResults = sParseNumber("2");

Console.WriteLine("sResult = {0}", sResults.Value);

#region Results with Match() usage
var sValue = sResults.Match(
    onSuccess: value => value,
    onFailure: error =>
    {
        Console.WriteLine("sError = {0}", error);
        return 0;
    }
);
Console.WriteLine("sValue = {0}", Value);
#endregion
#region Result with Match() without returned value
sResults.Match(value =>
{
    Console.WriteLine("sResult = {0}", value);
},
    error => {
        Console.WriteLine("sError = {0}", error);
    });
Console.WriteLine("sTesting Results<T> with failure.");
#endregion
#endregion

#region Basic Failure Usage
sResults = sParseNumber("x");

Console.WriteLine("sResult = {0}", sResults.Error);
#region Results with Match() usage
Value = sResults.Match(
    onSuccess: value => value,
    onFailure: error =>
    {
        Console.WriteLine("sError = {0}", error);
        return 0;
    }
);
Console.WriteLine("sValue = {0}", Value);
#endregion
#region Result with Match() without returned value
sResults.Match(value =>
    {
        Console.WriteLine("sResult = {0}", value);
    },
    error => {
        Console.WriteLine("sError = {0}", error);
    });
#endregion
#endregion

#region Example Functions
static sResults<int> sParseNumber(string input)
{
    if (int.TryParse(input, out int number))
        return sResults<int>.Success(number);
    return sResults<int>.Failure(Error.GenericFailure("GE-01","Generic Error"));
}
#endregion
#endregion


#region Results Usage

Results boolResult = CompareNumber(1, 2);

boolResult.Match(() =>
    {
        Console.WriteLine("bSuccess");
    }, error => {
    Console.WriteLine("bFailure with {0}", error);
    });

#region Example Functions
static Results CompareNumber(int inputA, int inputB)
{
    if (inputA == inputB)
        return Results.Success();
    return Results.Failure(StandardErrorType.GenericFailure);
}
#endregion
#endregion


#region sResults Usage
sResults sboolResult = sCompareNumber(1, 2);

sboolResult.Match(() =>
{
    Console.WriteLine("sbSuccess");
}, error => {
    Console.WriteLine("sbFailure with {0}", error);
});

#region Example Functions
static sResults sCompareNumber(int inputA, int inputB)
{
    if (inputA == inputB)
        return LCSoft.Results.sResults.Success();
    return LCSoft.Results.sResults.Failure(Error.GenericFailure("GE-02","Generic Error"));
}
#endregion

#endregion

#region Advanced Error Generation

// This example shows how to create custom error types and use them with Results<T>.

Results<int> aResults = aParseNumber("a");
aResults.Match(value =>
    {
        Console.WriteLine("aResult = {0}", value);
    },
    error => {
        Console.WriteLine("aError = {0}", error);
    });


#region Example Functions
static Results<int> aParseNumber(string input)
{
    if (int.TryParse(input, out int number))
        return Results<int>.Success(number);
    return Results<int>.Failure(NewErrorType.CustomError);
}
#endregion
#region New ErrorsType
class NewErrorType : ErrorsType
{
    public static readonly NewErrorType CustomError = new(1, "CustomError");
    private NewErrorType(int code, string name) : base(code, name)
    {
        Register(this);
    }
}

#endregion
#endregion