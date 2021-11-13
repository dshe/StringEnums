using System;
using Xunit;

namespace StringEnums.Tests
{
    public class Example
    {
        public sealed class SecurityType : StringEnum<SecurityType>
        {
            public static SecurityType Undefined { get; } = Create("");
            public static SecurityType Cash { get; } = Create("C");
            public static SecurityType Stock { get; } = Create("STK");
            public static SecurityType Bond { get; } = Create("BOND", "BND");
        }

        [Fact]
        public void T01_BasicUsage()
        {
            Assert.Equal("C", SecurityType.Cash.ToString());

            Assert.Equal(SecurityType.Cash, SecurityType.ToStringEnum("C"));
            Assert.Null(SecurityType.ToStringEnum("not found"));
        }

        [Fact]
        public void T02_MultipleStringValues()
        {
            Assert.Equal(SecurityType.Bond, SecurityType.ToStringEnum("BOND"));
            Assert.Equal(SecurityType.Bond, SecurityType.ToStringEnum("BND"));

            Assert.Equal("BOND", SecurityType.Bond.ToString());

            Assert.Equal(new[] { "BOND", "BND" }, SecurityType.Bond.ToStrings());
        }

        [Fact]
        public void T03_NewConstants()
        {
            SecurityType? newSecurityType = SecurityType.Add("New SecurityType");
            Assert.NotNull(newSecurityType);

            Assert.Equal(newSecurityType, SecurityType.ToStringEnum("New SecurityType"));

            Assert.Equal("New SecurityType", newSecurityType!.ToString());

            Assert.Equal(new[] { SecurityType.Undefined, SecurityType.Cash, SecurityType.Stock, SecurityType.Bond, newSecurityType },
                SecurityType.ToStringEnums());
        }

        [Fact]
        public void T04_StringCase()
        {
            Assert.Null(SecurityType.ToStringEnum("stk"));

            SecurityType.SetStringComparer(StringComparer.OrdinalIgnoreCase);
            Assert.Equal(SecurityType.Stock, SecurityType.ToStringEnum("stk"));
        }

        [Fact]
        public void T05_Extensions()
        {
            Assert.Equal(SecurityType.Cash, "C".ToStringEnum<SecurityType>());

            Assert.True(SecurityType.Cash.GetType().IsStringEnum());
        }
    }
}
