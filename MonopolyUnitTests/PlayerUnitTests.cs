using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Monopoly;
using Monopoly.Locations;

namespace MonopolyUnitTests
{

    [TestFixture]
    public class PlayerUnitTests
    {
        private IPlayer player;
        private Board board;
        private ILocation startingLocation;

        [SetUp]
        public void Init()
        {
            board = new Board();
            startingLocation = new Location();
            player = new Player(startingLocation, board);
        }

        [Test]
        public void Initialize_Player_To_Location_Zero()
        {
            Assert.AreEqual(player.PlayerLocation.SpaceNumber, 0);
        }

        [Test]
        public void Move_Player_Correctly_Adjusts_Players_Location()
        {
            board.locationManager.MovePlayer(player, 5);

            Assert.AreEqual(5, player.PlayerLocation.SpaceNumber);
        }

        [Test]
        public void Move_Player_Multiple_Times_Correctly_Adjusts_Players_Location()
        {
            board.locationManager.MovePlayer(player, 10);
            board.locationManager.MovePlayer(player, 15);
            board.locationManager.MovePlayer(player, 10);

            Assert.AreEqual(35, player.PlayerLocation.SpaceNumber);
        }
    }
}
