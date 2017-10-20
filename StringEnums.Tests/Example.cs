using System;
using System.Collections.Generic;
using Xunit;

namespace StringEnums.Tests
{
    public sealed class OrderType : StringEnum<OrderType>
    {
        public static readonly OrderType Market = Create("MARKET");
        public static readonly OrderType Limit = Create("LIMIT");
    }

    public class Example1
    {
        [Fact]
        public void Test()
        {
            Assert.Equal("MARKET", OrderType.Market.ToString());

            Assert.Equal(OrderType.Market, OrderType.ToStringEnum("MARKET"));
        }
    }

    public sealed class Location : StringEnum<Location>
    {
        public static readonly Location Undefined = Create("");
        public static readonly Location Europe = Create("Europe");
        public static readonly Location America = Create("America", "USA");
    }

    public class Example2
    {
        [Fact]
        public void Test1()
        {
            Assert.Equal(Location.America, Location.ToStringEnum("America"));
            Assert.Equal(Location.America, Location.ToStringEnum("USA"));

            Assert.Equal("America", Location.America.ToString());

            Assert.Equal(new List<string> { "America", "USA" }, Location.America.ToStrings());
        }

        [Fact]
        public void Test2()
        {
            Assert.False(Location.Undefined.IsNewValue);
            Assert.False(Location.Europe.IsNewValue);
            Assert.False(Location.America.IsNewValue);

            Location newLocation = Location.ToStringEnum("NEW VALUE");
            Assert.True(newLocation.IsNewValue);
        }

        [Fact]
        public void Test3()
        {
            Assert.Equal(new List<Location> { Location.Undefined, Location.Europe, Location.America }, Location.ToStringEnums());

            Location.SetStringComparer(StringComparer.OrdinalIgnoreCase);
            Assert.Equal(Location.ToStringEnum("SomeNewValue"), Location.ToStringEnum("SOMENewValue"));

            Assert.True(Location.America.GetType().IsStringEnum());
        }
    }
}
