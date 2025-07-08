using System;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace LCSoft.Results.Tests
{
    public class CustomTestFramework : XunitTestFramework
    {
        public CustomTestFramework(IMessageSink messageSink)
            : base(messageSink)
        {
            var isCI = Environment.GetEnvironmentVariable("CI")?.ToLowerInvariant() == "true"
                    || Environment.GetEnvironmentVariable("GITHUB_ACTIONS") == "true"
                    || Environment.GetEnvironmentVariable("TF_BUILD") == "true" // Azure DevOps
                    || Environment.GetEnvironmentVariable("BUILD_ID") != null;  // Generic CI

            if (!isCI)
            {
                Console.WriteLine("🧪 Running in LOCAL DEV → disabling test parallelism");
                AppDomain.CurrentDomain.SetData("xunit.parallelizeAssembly", false);
                AppDomain.CurrentDomain.SetData("xunit.parallelizeTestCollections", false);
            }
            else
            {
                Console.WriteLine("🚀 Running in CI → keeping default parallelism");
            }
        }
    }
}
