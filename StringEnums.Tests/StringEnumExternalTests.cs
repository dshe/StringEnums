using System;
using System.Reflection;
using Xunit;
using Xunit.Abstractions;

namespace StringEnums.Tests
{
    public sealed class StringEnumA : StringEnum<StringEnumA>
    {
        public static readonly StringEnumA Name1 = Create("1");
        public static readonly StringEnumA Name2 = Create("2", "3");
        public static readonly StringEnumA Name4 = Create("4");
    }

    public class StringEnumExternalTest
    {
        protected readonly Action<string> Write;
        public StringEnumExternalTest(ITestOutputHelper output) => Write = output.WriteLine;

        [Fact]
        public void T01_Value()
        {
            Assert.Equal("1", StringEnumA.Name1.ToString());
            Assert.False(StringEnumA.Name1.IsNewValue);
            Assert.Equal(StringEnumA.Name1, StringEnumA.ToStringEnum("1"));

            Assert.Equal("2", StringEnumA.Name2.ToString());
            Assert.False(StringEnumA.Name2.IsNewValue);
            Assert.Equal(StringEnumA.Name2, StringEnumA.ToStringEnum("2"));

            Assert.Equal("4", StringEnumA.Name4.ToString());
            Assert.False(StringEnumA.Name4.IsNewValue);
            Assert.Equal(StringEnumA.Name4, StringEnumA.ToStringEnum("4"));
        }

        [Fact]
        public void T02_MultipleValues()
        {
            var e = StringEnumA.ToStringEnum("3");
            Assert.Equal(StringEnumA.Name2, e);
            Assert.False(e.IsNewValue);
        }

        [Fact]
        public void T03_NewValue()
        {
            var empty = StringEnumA.ToStringEnum("");
            Assert.Equal("", empty.ToString());
            Assert.True(empty.IsNewValue);
            Assert.Equal(empty, StringEnumA.ToStringEnum(""));

            var e = StringEnumA.ToStringEnum("x");
            Assert.Equal("x", e.ToString());
            Assert.True(e.IsNewValue);
        }

        [Fact]
        public void T04_ToStringEnum()
        {
            Assert.Throws<ArgumentNullException>(() => StringEnumA.ToStringEnum(null));
            var e = StringEnumA.ToStringEnum("");
            Assert.Equal("", e.ToString());
            Assert.True(e.IsNewValue);
        }

        [Fact]
        public void T05_Constructor()
        {
            var nullEnum = new StringEnumA();
            Assert.Null(nullEnum.ToString());
            Assert.False(nullEnum.IsNewValue);
        }

        [Fact]
        public void T06_Extensions()
        {
            Assert.True(typeof(StringEnumA).IsStringEnum());
            Assert.True(typeof(StringEnumA).GetTypeInfo().IsStringEnum());

            Assert.Equal(StringEnumA.Name1, "1".ToStringEnum<StringEnumA>());
            Assert.Equal(StringEnumA.Name2, "3".ToStringEnum<StringEnumA>());
            var newValue = "new value".ToStringEnum<StringEnumA>();
        }

    }
}
