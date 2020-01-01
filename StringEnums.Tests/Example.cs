using System;
using Xunit;

namespace StringEnums.Tests
{
    public class Example
    {
        public sealed class SecurityType : StringEnum<SecurityType>
        {
            public static readonly SecurityType Undefined = Create("");
            public static readonly SecurityType Cash = Create("C");
            public static readonly SecurityType Stock = Create("STK");
            public static readonly SecurityType Bond = Create("BOND", "BND");
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
            if (newSecurityType == null)
                throw new Exception("null");

            Assert.Equal(newSecurityType, SecurityType.ToStringEnum("New SecurityType"));

            Assert.Equal("New SecurityType", newSecurityType.ToString());

            Assert.Equal(new[] { SecurityType.Undefined, SecurityType.Cash, SecurityType.Stock, SecurityType.Bond, newSecurityType },
                SecurityType.ToStringEnums());
        }

        [Fact]
        public void T04_StringCase()
        {
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
