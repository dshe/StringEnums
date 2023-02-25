using System.Reflection;

namespace StringEnums.Tests;

public class MainTests : TestBase
{
    public MainTests(ITestOutputHelper output) : base(output) { }

    public sealed class TestStringEnum : StringEnum<TestStringEnum>
    {
        public static TestStringEnum Name1 { get; } = Create("1");
        public static TestStringEnum Name2 { get; } = Create("2", "3");
        public static TestStringEnum Name4 { get; } = Create("4");
    }

    [Fact]
    public void T01_ToStringEnum()
    {
        Assert.Equal(TestStringEnum.Name1, TestStringEnum.ToStringEnum("1"));
        Assert.Equal(TestStringEnum.Name2, TestStringEnum.ToStringEnum("2"));
        Assert.Equal(TestStringEnum.Name2, TestStringEnum.ToStringEnum("3"));
        Assert.Equal(TestStringEnum.Name4, TestStringEnum.ToStringEnum("4"));
        Assert.Null(TestStringEnum.ToStringEnum("not found"));
    }

    [Fact]
    public void T02_ToStringEnums()
    {
        Assert.Equal(new[] { TestStringEnum.Name1, TestStringEnum.Name2, TestStringEnum.Name4 },
            TestStringEnum.ToStringEnums());
    }

    [Fact]
    public void T03_ToString()
    {
        Assert.Equal("1", TestStringEnum.Name1.ToString());
        Assert.Equal("2", TestStringEnum.Name2.ToString());
        Assert.Equal("4", TestStringEnum.Name4.ToString());
    }

    [Fact]
    public void T04_ToStrings()
    {
        Assert.Equal(new[] { "1" }, TestStringEnum.Name1.ToStrings());
        Assert.Equal(new[] { "2", "3" }, TestStringEnum.Name2.ToStrings());
        Assert.Equal(new[] { "4" }, TestStringEnum.Name4.ToStrings());
    }

    [Fact]
    public void T07_Extensions()
    {
        Assert.True(typeof(TestStringEnum).IsStringEnum());
        Assert.True(typeof(TestStringEnum).GetTypeInfo().IsStringEnum());

        Assert.Equal(TestStringEnum.Name1, "1".ToStringEnum<TestStringEnum>());
    }
}
