using Xunit;

namespace StringEnums.Tests
{
    public class AddTests
    {
        public sealed class TestStringEnum : StringEnum<TestStringEnum>
        {
            public static readonly TestStringEnum Name1 = Create("first");
        }

        [Fact]
        public void T00_Add()
        {
            const string newString = "new string";

            TestStringEnum newConstant = TestStringEnum.Add(newString);
            Assert.NotNull(newConstant);

            Assert.Null(TestStringEnum.Add(newString)); // string already exists
            Assert.Null(TestStringEnum.Add("another", newString)); // string already exists

            Assert.Equal(newString, newConstant.ToString());

            Assert.Equal(newConstant, TestStringEnum.ToStringEnum(newString));

            Assert.Equal(new[] { TestStringEnum.Name1, newConstant },
                TestStringEnum.ToStringEnums());
        }
    }
}
