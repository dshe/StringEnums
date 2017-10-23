using System;
using System.Collections.Generic;
using Xunit;

namespace StringEnums.Tests
{
    public class Example
    {
        public sealed class Location : StringEnum<Location>
        {
            public static readonly Location Undefined = Create("");
            public static readonly Location Europe = Create("Europe");
            public static readonly Location America = Create("America", "USA");
        }

        [Fact]
        public void T01_BasicUsage()
        {
            Assert.Equal("Europe", Location.Europe.ToString());

            Assert.Equal(Location.Europe, Location.ToStringEnum("Europe"));
            Assert.Null(Location.ToStringEnum("not found"));
        }

        [Fact]
        public void T02_MultipleStringValues()
        {
            Assert.Equal(Location.America, Location.ToStringEnum("America"));
            Assert.Equal(Location.America, Location.ToStringEnum("USA"));

            Assert.Equal("America", Location.America.ToString());

            Assert.Equal(new List<string> { "America", "USA" }, Location.America.ToStrings());
        }

        [Fact]
        public void T03_NewConstants()
        {
            Location newLocation = Location.Add("New Location");
            Assert.NotNull(newLocation);

            Assert.Equal(newLocation, Location.ToStringEnum("New Location"));

            Assert.Equal("New Location", newLocation.ToString());

            Assert.Equal(new List<Location> { Location.Undefined, Location.Europe, Location.America, newLocation },
                Location.ToStringEnums());
        }

        [Fact]
        public void T04_StringCase()
        {
            Location.SetStringComparer(StringComparer.OrdinalIgnoreCase);
            Assert.Equal(Location.Europe, Location.ToStringEnum("EUROPE"));
        }

        [Fact]
        public void T05_Extensions()
        {
            Assert.Equal(Location.Europe, "Europe".ToStringEnum<Location>());

            Assert.True(Location.America.GetType().IsStringEnum());
        }
    }
}
