using System;
using Xunit;

namespace StringEnums.Tests
{
    public class CreateTests : StringEnum<CreateTests>
    {
        [Fact]
        public void T01_CreateExceptions()
        {
            Assert.Throws<ArgumentException>(() => Create());
            Assert.Throws<ArgumentException>(() => Create(new string[] { }));
            Create(""); // empty string is ok
        }

        [Fact]
        public void T02_CreateOne()
        {
            var constant = Create("A");
            Assert.Equal("A", constant.ToString());
            Assert.Equal(constant, ToStringEnum("A"));
        }

        [Fact]
        public void T03_CreateMultiple()
        {
            var constant = Create("B", "C"); // multiple
            Assert.Equal("B", constant.ToString()); // the first string represents the value
            Assert.Equal(constant, ToStringEnum("B"));
            Assert.Equal(constant, ToStringEnum("C"));
        }

        [Fact]
        public void T04_CreateDuplicate()
        {
            Create("str"); // empty string is ok
            Assert.Throws<ArgumentException>(() => Create("str")); // duplicate
            Assert.Throws<ArgumentException>(() => Create("ok", "str")); // duplicate
        }
    }
}
