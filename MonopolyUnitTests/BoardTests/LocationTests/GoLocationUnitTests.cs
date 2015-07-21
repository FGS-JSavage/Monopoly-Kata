using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Board;
using Monopoly.Board.Locations;
using Monopoly.Tasks;
using NUnit.Framework;

namespace MonopolyUnitTests.TestClasses
{
    [TestFixture]
    class GoLocationUnitTests
    {
        private GoLocation goLocation;

        [Test]
        public void GoLocationInitializesWithCorrectSpaceNumber()
        {
            goLocation = new GoLocation();
            var expectedSpaceNumber = 0;

            Assert.AreEqual(goLocation.SpaceNumber, expectedSpaceNumber);
        }

        [Test]
        public void GoLocationInitializesWithCorrectTasks()
        {
            goLocation = new GoLocation();

            Assert.True(goLocation.GetOnLandTasks()[0].GetType() == typeof(LandOnGoTask));
        }

        [Test]
        public void GoLocationInitializesWithCorrectGroup()
        {
            goLocation = new GoLocation();
            var expectedGroup = PropertyGroup.Go;

            Assert.AreEqual(goLocation.Group, expectedGroup);
        }
    }
}
