using System;
using Xunit;

namespace StringEnums.Tests
{
    public sealed class StringEnumB : StringEnum<StringEnumB> {}

    public sealed class StringEnumC : StringEnum<StringEnumC> {}

    public class StringEnumStringComparerTests
    {
        [Fact]
        public void T00_Comparer()
        {
            Assert.NotEqual(StringEnumB.ToStringEnum("SomeNewValue"), StringEnumB.ToStringEnum("SOMENewValue"));
        }

        [Fact]
        public void T01_Comparer()
        {
            StringEnumC.SetComparer(StringComparer.OrdinalIgnoreCase);
            Assert.Equal(StringEnumC.ToStringEnum("SomeNewValue"), StringEnumC.ToStringEnum("SOMENewValue"));
        }

    }

}
