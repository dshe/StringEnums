using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace StringEnums.Tests;

public class PrimitiveEnumTests : TestBase
{
    public PrimitiveEnumTests(ITestOutputHelper output) : base(output) { }

    private enum TestEnum { Two = 2 }
    private static readonly Type Type = typeof(TestEnum);
    private static readonly TypeInfo TypeInfo = Type.GetTypeInfo();

    [Fact]
    public void T01_Type()
    {
        Assert.IsType<TestEnum>(TestEnum.Two);
        Assert.True(TypeInfo.IsEnum);
    }

    [Fact]
    public void T02_Get_Name() // enum to name
    {
        Assert.Equal("Two", TestEnum.Two.ToString());

        Assert.Equal("Two", Enum.GetName(Type, TestEnum.Two));
        Assert.Equal("Two", Enum.GetNames(Type).Single());

        Assert.Equal("Two", TypeInfo.GetEnumName(TestEnum.Two));
        Assert.Equal("Two", TypeInfo.GetEnumNames().Single());
    }

    [Fact]
    public void T03_Get_Value() // number to enum
    {
        Assert.Equal(TestEnum.Two, Enum.ToObject(Type, 2));
        Assert.Throws<ArgumentException>(() => Enum.ToObject(Type, "2"));
        Assert.Throws<ArgumentException>(() => Enum.ToObject(Type, "Two"));
        Assert.Equal(TestEnum.Two, Enum.ToObject(Type, TestEnum.Two));
    }

    [Fact]
    public void T04_Get_Underlying() // enum to number
    {
        Assert.Equal(2, (int)TestEnum.Two);
        Assert.Equal(typeof(Int32), Enum.GetUnderlyingType(Type));
        Assert.Equal(typeof(Int32), TypeInfo.GetEnumUnderlyingType());
    }

    [Fact]
    public void T05_Parse() // string name or number to enum
    {
        string? someNull = null;
#pragma warning disable CS8604 // Possible null reference argument.
        Assert.Throws<ArgumentNullException>(() => Enum.Parse(Type, someNull));
#pragma warning restore CS8604 // Possible null reference argument.
        Assert.Throws<ArgumentException>(() => Enum.Parse(Type, ""));
        Assert.Throws<ArgumentException>(() => Enum.Parse(Type, "invalid"));

        Assert.Equal(TestEnum.Two, Enum.Parse(Type, "Two"));
        Assert.Equal(TestEnum.Two, Enum.Parse(Type, "2"));
        Assert.Equal(99, (int)Enum.Parse(Type, "99")); // new value

        Assert.False(Enum.TryParse(null, out TestEnum a));
        Assert.False(Enum.TryParse("", out TestEnum b));
        Assert.False(Enum.TryParse("invalid", out TestEnum c));

        Assert.True(Enum.TryParse("Two", out TestEnum d));
        Assert.True(Enum.TryParse("2", out TestEnum e));
        Assert.True(Enum.TryParse("99", out TestEnum f)); // new value
    }

    [Fact]
    public void T06_IsDefined() // object as string name, number or enum
    {
        string? someNull = null;
#pragma warning disable CS8604 // Possible null reference argument.
        Assert.Throws<ArgumentNullException>(() => Enum.IsDefined(Type, someNull));
#pragma warning restore CS8604 // Possible null reference argument.
        Assert.False(Enum.IsDefined(Type, ""));
        Assert.False(Enum.IsDefined(Type, "invalid"));

        Assert.True(Enum.IsDefined(Type, "Two"));
        Assert.False(Enum.IsDefined(Type, "2")); // note!
        Assert.True(Enum.IsDefined(Type, 2));

        Assert.False(Enum.IsDefined(Type, 99));
        Assert.True(Enum.IsDefined(Type, TestEnum.Two)); // obviously
        Assert.False(Enum.IsDefined(Type, Enum.Parse(Type, "99"))); // new value

        Assert.True(TypeInfo.IsEnumDefined(2));
    }

    [Fact]
    public void T07_Get_Values() // number to enum
    {
        List<TestEnum> values = Enum.GetValues(Type).OfType<TestEnum>().ToList();
        Assert.Equal(values, TypeInfo.GetEnumValues().OfType<TestEnum>().ToList());
        Assert.Equal(TestEnum.Two, values.Single());
    }
}
