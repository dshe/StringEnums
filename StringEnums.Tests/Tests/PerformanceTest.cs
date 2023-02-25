using System.Runtime.Serialization;
using Microsoft.Extensions.Logging;

namespace StringEnums.Tests;

public class PerformanceTests : TestBase
{
    public PerformanceTests(ITestOutputHelper output) : base(output) { }

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

    [Fact]
    public void Test()
    {
        Logger.LogDebug("Performance");

        Perf perf = new(x => Logger.LogDebug("{Info}", x));

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
