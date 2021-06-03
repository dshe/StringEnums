using System;
using System.Runtime.Serialization;
using Xunit;
using Xunit.Abstractions;
using StringEnums.Tests.Utility;
using Microsoft.Extensions.Logging;

namespace StringEnums.Tests
{
    public class PerformanceTests
    {
        public enum PrimitiveEnum
        {
            [EnumMember(Value = "")]
            Undefined,

            [EnumMember(Value = "MARKET")]
            Market,

            [EnumMember(Value = "LIMIT")]
            Limit,
        }

        public sealed class OrderType : StringEnum<OrderType>
        {
            public static readonly OrderType Undefined = Create("");
            public static readonly OrderType Market = Create("MARKET");
            public static readonly OrderType Limit = Create("LIMIT");
        }


        public readonly ILogger Logger;
        public PerformanceTests(ITestOutputHelper output)
        {
            var loggerFactory = new LoggerFactory().AddMXLogger(output.WriteLine);
            Logger = loggerFactory.CreateLogger();
        }

        [Fact]
        public void Test()
        {
            Logger.LogDebug("Performance");

            var perf = new Perf(x => Logger.LogDebug(x));

            perf.MeasureRate(() =>
            {
                Assert.Equal(PrimitiveEnum.Market, Enum.Parse<PrimitiveEnum>("Market"));
            }, "Parse primitive enum.");

            perf.MeasureRate(() =>
            {
                Assert.Equal(OrderType.Market, "MARKET".ToStringEnum<OrderType>());
            }, "Parse StringEnum.");

            perf.MeasureRate(() =>
            {
                Assert.Equal(PrimitiveEnum.Market, "MARKET".ToEnum<PrimitiveEnum>());
            }, "Parse primitive enum with EnumMember attribute.");
        }
    }
}
