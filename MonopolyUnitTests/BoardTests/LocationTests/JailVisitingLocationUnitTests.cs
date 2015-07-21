using Monopoly.Board;
using Monopoly.Board.Locations;
using NUnit.Framework;

namespace MonopolyUnitTests.BoardTests.LocationTests
{
    [TestFixture]
    class JailVisitingLocationUnitTests
    {
        private JailVisitingLocation jailVisitingLocation;

        [SetUp]
        public void Init()
        {
            jailVisitingLocation = new JailVisitingLocation();
        }

        [Test]
        public void JailVisitingLocationInitializesWithCorrectSpaceNumber()
        {
            var expectedSpaceNumber = 10;

            Assert.AreEqual(jailVisitingLocation.SpaceNumber, expectedSpaceNumber);
        }

        [Test]
        public void JailVisitingLocationInitializesWithCorrectTasks()
        {
            Assert.True(jailVisitingLocation.GetOnLandTasks().Count == 0);
        }

        [Test]
        public void JailVisitingLocationInitializesWithCorrectGroup()
        {
            var expectedGroup = PropertyGroup.JailVisiting;

            Assert.AreEqual(jailVisitingLocation.Group, expectedGroup);
        }
    }
}