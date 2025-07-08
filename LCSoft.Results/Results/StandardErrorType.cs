namespace LCSoft.Results
{
    public sealed class StandardErrorType : ErrorsType
    {
        public static readonly StandardErrorType GenericFailure = new(0, "GenericFailure");
        public static readonly StandardErrorType Validation = new(1, "Validation");
        public static readonly StandardErrorType NotFound = new(2, "NotFound");
        public static readonly StandardErrorType Conflict = new(3, "Conflict");
        public static readonly StandardErrorType ServerError = new(4, "ServerError");
        public static readonly StandardErrorType DomainError = new(5, "DomainError");

        private StandardErrorType(int code, string name) : base(code, name)
        {
            Register(this);
        }
    }
}