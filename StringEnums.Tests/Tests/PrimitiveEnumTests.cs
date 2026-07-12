using System.Linq;
using System.Reflection;
using System.Collections.Generic;
namespace StringEnums.Tests;

public class PrimitiveEnumTests : TestBase
{
    public PrimitiveEnumTests(ITestOutputHelper output) : base(output) { }

    private enum TestEnum { Two = 2 }
    private static readonly Type _type = typeof(TestEnum);
    private static readonly TypeInfo _typeInfo = _type.GetTypeInfo();

    [Fact]
    public void T01_Type()
    {
        Assert.IsType<TestEnum>(TestEnum.Two);
        Assert.True(_typeInfo.IsEnum);
    }

    [Fact]
    public void T02_Get_Name() // enum to name
    {
        Assert.Equal("Two", TestEnum.Two.ToString());

        Assert.Equal("Two", Enum.GetName(_type, TestEnum.Two));
        Assert.Equal("Two", Enum.GetNames(_type).Single());

        Assert.Equal("Two", _typeInfo.GetEnumName(TestEnum.Two));
        Assert.Equal("Two", _typeInfo.GetEnumNames().Single());
    }

    [Fact]
    public void T03_Get_Value() // number to enum
    {
        Assert.Equal(TestEnum.Two, Enum.ToObject(_type, 2));
        Assert.Throws<ArgumentException>(() => Enum.ToObject(_type, "2"));
        Assert.Throws<ArgumentException>(() => Enum.ToObject(_type, "Two"));
        Assert.Equal(TestEnum.Two, Enum.ToObject(_type, TestEnum.Two));
    }

    [Fact]
    public void T04_Get_Underlying() // enum to number
    {
        Assert.Equal(2, (int)TestEnum.Two);
        Assert.Equal(typeof(Int32), Enum.GetUnderlyingType(_type));
        Assert.Equal(typeof(Int32), _typeInfo.GetEnumUnderlyingType());
    }

    [Fact]
    public void T05_Parse() // string name or number to enum
    {
        string? someNull = null;
#pragma warning disable CS8604 // Possible null reference argument.
        Assert.Throws<ArgumentNullException>(() => Enum.Parse(_type, someNull));
#pragma warning restore CS8604 // Possible null reference argument.
        Assert.Throws<ArgumentException>(() => Enum.Parse(_type, ""));
        Assert.Throws<ArgumentException>(() => Enum.Parse(_type, "invalid"));

        Assert.Equal(TestEnum.Two, Enum.Parse(_type, "Two"));
        Assert.Equal(TestEnum.Two, Enum.Parse(_type, "2"));
        Assert.Equal(99, (int)Enum.Parse(_type, "99")); // new value

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
        Assert.Throws<ArgumentNullException>(() => Enum.IsDefined(_type, someNull));
#pragma warning restore CS8604 // Possible null reference argument.
        Assert.False(Enum.IsDefined(_type, ""));
        Assert.False(Enum.IsDefined(_type, "invalid"));

        Assert.True(Enum.IsDefined(_type, "Two"));
        Assert.False(Enum.IsDefined(_type, "2")); // note!
        Assert.True(Enum.IsDefined(_type, 2));

        Assert.False(Enum.IsDefined(_type, 99));
        Assert.True(Enum.IsDefined(_type, TestEnum.Two)); // obviously
        Assert.False(Enum.IsDefined(_type, Enum.Parse(_type, "99"))); // new value

        Assert.True(_typeInfo.IsEnumDefined(2));
    }

    [Fact]
    public void T07_Get_Values() // number to enum
    {
        List<TestEnum> values = Enum.GetValues(_type).OfType<TestEnum>().ToList();
        Assert.Equal(values, _typeInfo.GetEnumValues().OfType<TestEnum>().ToList());
        Assert.Equal(TestEnum.Two, values.Single());
    }
}
