using System;
using Monopoly.Board;
using Monopoly.Board.Locations;
using NUnit.Framework;

namespace MonopolyUnitTests.BoardTests.LocationTests
{
    [TestFixture]
    class LocationUnitTests
    {
        private Location location;

        [Test]
        [TestCase(  0, Result =  0 )]
        [TestCase(  5, Result =  5 )]
        [TestCase( -5, Result = -5 )]
        [TestCase( 45, Result = 45 )]
        public int ConstructorInitializesSpaceNumberCorrectly(int spaceNumber)
        {
            location = new Location(spaceNumber, PropertyGroup.Go);

            return location.SpaceNumber;
        }

        [Test]
        [TestCase( PropertyGroup.Chance,    Result = PropertyGroup.Chance    )]
        [TestCase( PropertyGroup.Chest,     Result = PropertyGroup.Chest     )]
        [TestCase( PropertyGroup.DarkBlue,  Result = PropertyGroup.DarkBlue  )]
        [TestCase( PropertyGroup.DarkGreen, Result = PropertyGroup.DarkGreen )]
        public PropertyGroup ConstructorInitializesPropertyGroupCorrectly(PropertyGroup group)
        {
            location = new Location(0, group);

            return location.Group;
        }
    }
}
