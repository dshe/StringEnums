using System;
using System.Reflection;
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

            Assert.Equal(OrderType.Market, OrderType.Parse("MARKET"));

            Assert.False(OrderType.Market.IsNewValue);

            var newValue = OrderType.Parse("SOME NEW VALUE");
            Assert.True(newValue.IsNewValue);

            StringEnumC.SetComparer(StringComparer.OrdinalIgnoreCase);
            Assert.Equal(StringEnumC.Parse("SomeNewValue"), StringEnumC.Parse("SOMENewValue"));

            OrderType xx = new OrderType();
            var xbx = xx.GetType();
            Assert.True(OrderType.Market.GetType().IsStringEnum());
            Assert.True(OrderType.Market.GetType().GetTypeInfo().IsStringEnum());
            ;
        }


    }

}
