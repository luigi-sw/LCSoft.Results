# LCSoft: Results Pattern for .NET

A robust, flexible, and type-safe result and error handling library for .NET applications.
Provides unified success/failure result types with rich error information and functional pattern matching for clean, expressive code.

[![NuGet](https://img.shields.io/nuget/v/Results.Pattern.svg)](https://www.nuget.org/packages/LCSoft.Results/)
[![License: CC BY-NC-ND 4.0](https://img.shields.io/badge/License-CC_BY--NC--ND_4.0-lightgrey.svg)](https://creativecommons.org/licenses/by-nc-nd/4.0/)

---

## ✨ Features

- Unified result types for success/failure handling
- Generic Results\<TValue> and non-generic Results classes
- Simplified alternative via sResults\<TValue> and sResults using the lightweight Error record
- Encapsulation of success value or error with rich error details
- Functional **Match** methods supporting both **Func** and **Action** delegates
- Implicit conversion operators for easy construction from values or errors
- Abstract **ErrorsType** base class with error code, name, and registry
- Standard predefined error types via **StandardErrorType**
- Detailed **Error** record with error code, message, and categorized error type
- **ErrorType** enum classifying error categories
- Interfaces IResult\<TError> and IResult<TValue, TError> with default implementations
- Explicit interface implementations for polymorphic usage
- Centralized error code registry and lookup by code
- Nullable callback support in Match methods
- Designed for testability and code coverage compatibility
- Extensible architecture for custom error and result handling
- Suitable for layered and domain-driven design applications

---

## 📦 Installation

Install via NuGet:

```bash
dotnet add package LCSoft.Results
```

Or via Package Manager:

```bash
Install-Package LCSoft.Results
```

## 🚀 Quick Start

### Basic Usage

```csharp
using LCSoft.Results;

Results result = DoSomething();

if (result.IsSuccess)
{
    Console.WriteLine("Success!");
}
else
{
    Console.WriteLine($"Failed: {result.Error}");
}
```

With a Value

```csharp
Results<int> result = Calculate();

if (result.IsSuccess)
{
    int value = result.Value;
    Console.WriteLine($"Result: {value}");
}
else
{
    Console.WriteLine($"Error: {result.Error}");
}
```

### Non-generic Result

```csharp
Results result = Results.Success();

result.Match(
    success: () => Console.WriteLine("Operation succeeded!"),
    failure: error => Console.WriteLine($"Failed with error: {error.Name}")
);
```
### Generic Result with Value

```csharp
Results<int> result = 42; // implicit success

int value = result.Match(
    onSuccess: val => val * 2,
    onFailure: error => -1
);
Console.WriteLine(value); // 84
```

### Handling Errors

```csharp
var error = StandardErrorType.Validation;
Results failureResult = Results.Failure(error);

failureResult.Match(
    success: () => Console.WriteLine("This won't be called."),
    failure: err => Console.WriteLine($"Validation failed: {err.Name}")
);
```

### Using sResults and sResults<T>

For lighter scenarios, you can use sResults and sResults\<T> which use a simplified Error record structure.

```csharp
sResults<int> result = 10;

result.Match(
    success: val => Console.WriteLine($"Success with value {val}"),
    failure: err => Console.WriteLine($"Failed with code {err.Code}")
);
```

### Extending Errors

```csharp
public sealed class CustomErrorType : ErrorsType
{
    public static readonly CustomErrorType CustomFailure = new(100, "CustomFailure");

    private CustomErrorType(int code, string name) : base(code, name)
    {
        Register(this);
    }
}
```

Use ErrorsType.FromCode(code) to retrieve error types dynamically.

## Interfaces

- IResult\<TError> for basic result contracts
- IResult<TValue, TError> for generic result contracts
- Both provide functional **Match** methods with success and failure delegates

## ✅ Example

```csharp
Results<int> ParseNumber(string input)
{
    if (int.TryParse(input, out int number))
        return Result<int>.Success(number);
    return Result<int>.Failure("Invalid number format");
}

Results<int> CalculateSomething(int number)
{
    if (number < 0)
        return Result<int>.Failure("Negative number not allowed");
    return Result<int>.Success(number * 10);
}
```


## 📄 License

This project is licensed under the MIT License. See the LICENSE file for details.

## 🙌 Contributing

Contributions are welcome! Please open issues or submit pull requests on GitHub.
