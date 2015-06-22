using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Monopoly;

namespace MonopolyUnitTests
{

    [TestFixture]
    public class PlayerUnitTests
    {
        private IPlayer player;
        private ILocation startingLocation = new Location();

        [Test]
        public void Initialize_Player_To_Location_Zero()
        {
            player = new Player(startingLocation);

            Assert.AreEqual(player.GetLocation().GetSpaceNumber(), 0);
        }

        [Test]
        public void Move_Player_Correctly_Adjusts_Players_Location()
        {
            player = new Player(startingLocation);

            player.MoveDistance(5);

            Assert.AreEqual(player.GetLocation().GetSpaceNumber(), 5);
        }

        [Test]
        public void Move_Player_Multiple_Times_Correctly_Adjusts_Players_Location()
        {
            player = new Player(startingLocation);

            player.MoveDistance(5);
            player.MoveDistance(10);
            player.MoveDistance(15);
            player.MoveDistance(10);

            Assert.AreEqual(player.GetLocation().GetSpaceNumber(), 5);
        }

    }
}
