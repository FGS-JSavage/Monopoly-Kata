using Monopoly.Board;
using Monopoly.Board.Locations;
using Monopoly.Tasks;
using NUnit.Framework;

namespace MonopolyUnitTests.BoardTests.LocationTests
{
    [TestFixture]
    class LuxuryTaxLocationUnitTests
    {
        private LuxuryTaxLocation luxuryTaxLocation;

        [SetUp]
        public void Init()
        {
            luxuryTaxLocation = new LuxuryTaxLocation();
        }

        [Test]
        public void LuxuryTaxLocationInitializesWithCorrectSpaceNumber()
        {
            var expectedSpaceNumber = 38;

            Assert.AreEqual(luxuryTaxLocation.SpaceNumber, expectedSpaceNumber);
        }

        [Test]
        public void LuxuryTaxLocationInitializesWithCorrectTasks()
        {
            Assert.True(luxuryTaxLocation.GetOnLandTasks()[0].GetType() == typeof(PayLuxuryTaxTask));
        }

        [Test]
        public void LuxuryTaxLocationInitializesWithCorrectGroup()
        {
            var expectedGroup = PropertyGroup.Tax;

            Assert.AreEqual(luxuryTaxLocation.Group, expectedGroup);
        }
    }
}
