using Monopoly.Board;
using Monopoly.Board.Locations;
using Monopoly.Tasks;
using NUnit.Framework;

namespace MonopolyUnitTests.BoardTests.LocationTests
{
    [TestFixture]
    class IncomeTaxLocationUnitTests
    {
        private IncomeTaxLocation incomeTaxLocation;

        [SetUp]
        public void Init()
        {
            incomeTaxLocation = new IncomeTaxLocation();
        }


        [Test]
        public void IncomeTaxLocationInitializesWithCorrectSpaceNumber()
        {
            var expectedSpaceNumber = 4;

            Assert.AreEqual(incomeTaxLocation.SpaceNumber, expectedSpaceNumber);
        }

        [Test]
        public void IncomeTaxLocationInitializesWithCorrectTasks()
        {
            Assert.True(incomeTaxLocation.GetOnLandTasks()[0].GetType() == typeof(PayIncomeTaxTask));
        }

        [Test]
        public void IncomeTaxLocationInitializesWithCorrectGroup()
        {
            var expectedGroup = PropertyGroup.Tax;

            Assert.AreEqual(incomeTaxLocation.Group, expectedGroup);
        }
    }
}
