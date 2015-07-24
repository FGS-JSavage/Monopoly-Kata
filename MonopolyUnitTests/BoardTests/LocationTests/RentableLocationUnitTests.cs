using Monopoly.Board;
using Monopoly.Board.Locations;
using NUnit.Framework;

namespace MonopolyUnitTests.BoardTests.LocationTests
{
    [TestFixture]
    class RentableLocationUnitTests
    {
        private RentableLocation location;

        [Test]
        [TestCase(   0, Result =   0 )]
        [TestCase(   5, Result =   5 )]
        [TestCase(  10, Result =  10 )]
        [TestCase( 100, Result = 100 )]
        public int ConstructorInitializesRentCorrectly(int rent)
        {
            int someSpaceNumber = 5;
            int somePrice = 5;
            PropertyGroup someGroup = PropertyGroup.DarkGreen;

            location = new RentableLocation(someSpaceNumber, rent, somePrice, someGroup);

            return location.Rent;
        }

        [Test]
        [TestCase(   0, Result =   0 )]
        [TestCase(   5, Result =   5 )]
        [TestCase(  10, Result =  10 )]
        [TestCase( 100, Result = 100 )]
        public int ConstructorInitializesPriceCorrectly(int price)
        {
            int someSpaceNumber = 5;
            int someRent = 5;
            PropertyGroup someGroup = PropertyGroup.DarkGreen;

            location = new RentableLocation(someSpaceNumber, someRent, price, someGroup);

            return location.Price;
        }
    }
}
