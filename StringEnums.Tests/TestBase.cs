using Microsoft.Extensions.Logging;

namespace StringEnums.Tests;

public abstract class TestBase
{
    protected readonly ILogger Logger;
    protected readonly Action<string> Write;

    protected TestBase(ITestOutputHelper output, LogLevel logLevel = LogLevel.Trace)
    {
        Logger = LoggerFactory
            .Create(builder => builder
                .AddMXLogger(output.WriteLine)
                .SetMinimumLevel(logLevel))
            .CreateLogger("Test");

        Write = (s) => output.WriteLine(s + "\r\n");
    }
}
