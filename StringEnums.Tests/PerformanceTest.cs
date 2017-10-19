using System;
using System.ComponentModel;
using Xunit;
using Xunit.Abstractions;

namespace StringEnums.Tests
{
    public class PerformanceTest
    {
        public enum OrderTypeE
        {
            [Description("")]
            Undefined,
            [Description("MARKET")]
            Market,
            [Description("LIMIT")]
            Limit,
            [Description("TRAILING LIMIT")]
            TrailingLimit
        }

        public sealed class OrderType : StringEnum<OrderType>
        {
            public static readonly OrderType Undefined = Create("");
            public static readonly OrderType Market = Create("MARKET");
            public static readonly OrderType Limit = Create("LIMIT");
            public static readonly OrderType TrailingLimit = Create("TRAIL LIMIT");
        }

        protected readonly Action<string> Write;
        public PerformanceTest(ITestOutputHelper output) => Write = output.WriteLine;

        [Fact]
        public void Perf()
        {
            Write("Performance");

            var perf = new Perf(Write);

            perf.MeasureRate(() =>
            {
                Assert.Equal(OrderTypeE.Market, Enum.Parse<OrderTypeE>("MARKET", ignoreCase:true));
            }, "Parse primitive enum.");

            perf.MeasureRate(() =>
            {
                Assert.Equal(OrderTypeE.Market, "MARKET".ToEnum<OrderTypeE>());
            }, "Parse primitive enum with description attribute.");

            perf.MeasureRate(() =>
            {
                Assert.Equal(OrderType.Market, "MARKET".ToStringEnum<OrderType>());
            }, "Parse StringEnum.");
        }
    }
}
