using System;
using Xunit;

namespace StringEnums.Tests
{
    public class StringComparerTests
    {
        public sealed class TestStringEnum : StringEnum<TestStringEnum> { }

        [Fact]
        public void T00_Comparer()
        {
            var constant = TestStringEnum.Add("string");

            Assert.Equal(constant, TestStringEnum.ToStringEnum("string"));
            Assert.Null(TestStringEnum.ToStringEnum("STRING"));

            TestStringEnum.SetStringComparer(StringComparer.OrdinalIgnoreCase);
            Assert.Equal(constant, TestStringEnum.ToStringEnum("STRING"));
        }
    }
}
