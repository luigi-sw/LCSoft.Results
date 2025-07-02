namespace LCSoft.Results.Tests
{
    public class ErrorTests
    {
        [Fact]
        public void EmptyFailure()
        {
            var error = Error.EmptyFailure;
        }

        [Fact]
        public void GenericFailure()
        {
            var error = Error.GenericFailure("GE-01", "Generic Error");

            Assert.Equal("GE-01", error.Code);
        }

        [Fact]
        public void ValidationFailure()
        {
            var error = Error.ValidationFailure("VA-01", "Generic Error");

            Assert.Equal("VA-01", error.Code);
        }

        [Fact]
        public void NotFoundFailure()
        {
            var error = Error.NotFoundFailure("NF-01", "Generic Error");

            Assert.Equal("NF-01", error.Code);
        }

        [Fact]
        public void ConflitFailure()
        {
            var error = Error.ConflitFailure("CF-01", "Generic Error");

            Assert.Equal("CF-01", error.Code);
        }

        [Fact]
        public void ServerErrorFailure()
        {
            var error = Error.ServerErrorFailure("SE-01", "Generic Error");

            Assert.Equal("SE-01", error.Code);
        }

        [Fact]
        public void DomainErrorFailure()
        {
            var error = Error.DomainErrorFailure("DO-01", "Generic Error");

            Assert.Equal("DO-01", error.Code);
        }
    }
}
