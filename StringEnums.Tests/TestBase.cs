using Microsoft.Extensions.Logging;

namespace StringEnums.Tests;

public abstract class TestBase(ITestOutputHelper output, LogLevel logLevel = LogLevel.Trace)
{
    protected readonly ILogger Logger = LoggerFactory
            .Create(builder => builder
                .AddMXLogger(output.WriteLine)
                .SetMinimumLevel(logLevel))
            .CreateLogger("Test");

    protected readonly Action<string> Write = (s) => output.WriteLine(s + "\r\n");
}
