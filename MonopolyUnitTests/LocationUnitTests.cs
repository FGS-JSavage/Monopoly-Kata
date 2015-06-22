using NUnit.Framework;
using Monopoly;

namespace MonopolyUnitTests
{
    [TestFixture]
    class LocationUnitTests
    {
        private Location location;

        [Test]
        public void Location_Initializes_To_Space_Zero()
        {
            location = new Location();

            Assert.AreEqual(location.GetSpaceNumber(), 0);
        }

        [Test]
        [TestCase(0, Result = 0)]
        [TestCase(1, Result = 1)]
        [TestCase(10, Result = 10)]
        [TestCase(39, Result = 39)]
        [TestCase(40, Result = 0)]
        [TestCase(45, Result = 5)]
        [TestCase(85, Result = 5)]
        public int Location_Move_Forward_Correct_Distance(int distance)
        {
            location = new Location();

            location.MoveFowrard(distance);

            return location.GetSpaceNumber();
        }

        [Test]
        public void Location_Move_Forward_Multiple_Times_Correct_Distance()
        {
            location = new Location();

            location.MoveFowrard(5);
            location.MoveFowrard(10);
            location.MoveFowrard(20);
            location.MoveFowrard(10);

            Assert.AreEqual(location.GetSpaceNumber(), 5);
        }

        [Test]
        [TestCase(0, Result = 0)]
        [TestCase(5, Result = 5)]
        [TestCase(39, Result = 39)]
        public int Jump_To_Space_Moves_To_Specified_Space_Number(int spaceNumber)
        {
            location = new Location();

            location.JumpToSpaceNumber(spaceNumber);

            return location.GetSpaceNumber();
        }
    }
}
