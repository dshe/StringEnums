using System;
using Xunit;

namespace StringEnums.Tests
{
    public sealed class OrderType : StringEnum<OrderType>
    {
        public static readonly OrderType Undefined = Create("");
        public static readonly OrderType Market = Create("MARKET");
        public static readonly OrderType Limit = Create("LIMIT");
        public static readonly OrderType TrailingLimit = Create("TRAIL LIMIT");
    }

    public class Example
    {
        [Fact]
        public void Test()
        {
            Assert.Equal("MARKET", OrderType.Market.ToString());

            Assert.Equal(OrderType.Market, OrderType.ToStringEnum("MARKET"));

            Assert.False(OrderType.ToStringEnum("MARKET").IsNewValue);
            Assert.True(OrderType.ToStringEnum("SOME NEW VALUE").IsNewValue);

            StringEnumC.SetComparer(StringComparer.OrdinalIgnoreCase);
            Assert.Equal(StringEnumC.ToStringEnum("SomeNewValue"), StringEnumC.ToStringEnum("SOMENewValue"));

            Assert.True(OrderType.Market.GetType().IsStringEnum());
        }


    }

}
