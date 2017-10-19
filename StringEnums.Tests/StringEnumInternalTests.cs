using System;
using Xunit;

namespace StringEnums.Tests
{
    public class StringEnumInternalTest : StringEnum<StringEnumInternalTest>
    {
        [Fact]
        public void T01_Null()
        {
            Assert.Throws<ArgumentException>(() => Create());
            Assert.Throws<ArgumentException>(() => Create(null));
            Assert.Throws<ArgumentException>(() => Create(new string[] { }));
            Assert.Throws<ArgumentException>(() => Create(new string[] { null }));
            Create(""); // ok
            Assert.Throws<ArgumentException>(() => Create("")); // duplicate
        }

        [Fact]
        public void T02_CreateOne()
        {
            var e = Create("A");
            Assert.Equal("A", e.ToString());
            Assert.False(e.IsNewValue);
            Assert.Equal(e, Parse("A"));
        }

        [Fact]
        public void T03_CreateMultiple()
        {
            var e = Create("B", "C");   // multiple
            Assert.Equal("B", e.ToString()); // the first represents the value
            Assert.False(e.IsNewValue);
            Assert.Equal(e, Parse("B"));
            Assert.Equal(e, Parse("C"));
        }

        [Fact]
        public void T04_ParseNew()
        {
            Assert.Throws<ArgumentNullException>(() => Parse(null));
            var e1 = Parse("D");
            Assert.True(e1.IsNewValue);
            Assert.Equal(e1, Parse("D"));
        }
    }
}
