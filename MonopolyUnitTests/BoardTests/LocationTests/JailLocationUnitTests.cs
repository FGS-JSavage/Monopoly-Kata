
using Monopoly.Board;
using Monopoly.Board.Locations;
using Monopoly.Tasks;
using NUnit.Framework;

namespace MonopolyUnitTests.BoardTests.LocationTests
{
    [TestFixture]
    class JailLocationUnitTests
    {
        private JailLocation jailLocation;

        [SetUp]
        public void Init()
        {
            jailLocation = new JailLocation();
        }


        [Test]
        public void JailLocationInitializesWithCorrectSpaceNumber()
        {
            var expectedSpaceNumber = 30;

            Assert.AreEqual(jailLocation.SpaceNumber, expectedSpaceNumber);
        }

        [Test]
        public void JailLocationInitializesWithCorrectTasks()
        {
            Assert.True(jailLocation.GetOnLandTasks().Count == 0);
        }

        [Test]
        public void JailLocationInitializesWithCorrectGroup()
        {
            var expectedGroup = PropertyGroup.Jail;

            Assert.AreEqual(jailLocation.Group, expectedGroup);
        }
    }
}