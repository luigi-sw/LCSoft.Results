namespace LC.Results;

public abstract class ErrorsType
{
    public int Code { get; }
    public string Name { get; }

    protected ErrorsType(int code, string name)
    {
        Code = code;
        Name = name;
    }

    public override string ToString() => Name;

    public override bool Equals(object obj)
    {
        return obj is ErrorsType other && Code == other.Code;
    }

    public override int GetHashCode() => Code.GetHashCode();

    private static readonly Dictionary<int, ErrorsType> _all = new();

    protected static void Register(ErrorsType errorType)
    {
        _all[errorType.Code] = errorType;
    }

    public static ErrorsType FromCode(int code) => _all.TryGetValue(code, out var val) ? val : null!;
}
