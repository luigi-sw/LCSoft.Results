# LC Results Pattern for .NET

A simple and expressive implementation of the Results Pattern for .NET applications. This package helps you model operation outcomes in a consistent, predictable way — avoiding exceptions for control flow and enabling robust error handling.

[![NuGet](https://img.shields.io/nuget/v/Results.Pattern.svg)](https://www.nuget.org/packages/Results.Pattern/)
[![License: CC BY-NC-ND 4.0](https://img.shields.io/badge/License-CC_BY--NC--ND_4.0-lightgrey.svg)](https://creativecommons.org/licenses/by-nc-nd/4.0/)

---

## ✨ Features

- Encapsulate operation success and failure
- Support for value and error propagation
- Strong typing and immutability
- Optional support for domain error types

---

## 📦 Installation

Install via NuGet:

```bash
dotnet add package LC.Results
```

Or via Package Manager:

```bash
Install-Package LC.Results
```

🚀 Quick Start
Basic Usage

```csharp
using LC.Results;

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

🛠 API Overview

Results

```csharp
public class Results
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    public static Result Success();
    public static Result Failure(Error error);
}
```

Results\<T>

```csharp
public class Results<T>
{
    public bool IsSuccess { get; }
    public T Value { get; }
    public Error Error { get; }

    public static Result<T> Success(T value);
    public static Result<T> Failure(Error error);
}
```


✅ Example

```csharp
Result<int> ParseNumber(string input)
{
    if (int.TryParse(input, out int number))
        return Result<int>.Success(number);
    return Result<int>.Failure("Invalid number format");
}

Result<int> CalculateSomething(int number)
{
    if (number < 0)
        return Result<int>.Failure("Negative number not allowed");
    return Result<int>.Success(number * 10);
}
```

📄 License

This project is licensed under the MIT License. See the LICENSE file for details.
🙌 Contributing

Contributions are welcome! Please open issues or submit pull requests on GitHub.

