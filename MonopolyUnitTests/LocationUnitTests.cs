using System.Collections.Generic;
using NUnit.Framework;
using Monopoly;
using Monopoly.Locations;

namespace MonopolyUnitTests
{
    [TestFixture]
    class LocationUnitTests
    {
        private Game game;
        private Board board;
        private List<IPlayer> players;
        private LocationManager locationManager;

        [SetUp]
        public void Init()
        {
            game = new Game();
            board = game.GetBoard();
            players = game.GetPlayers();
            locationManager = board.GetLocationManager();
        }

        [Test]
        public void Location_Initializes_To_Space_Zero()
        {
            Assert.AreEqual(players[0].PlayerLocation.SpaceNumber, 0);
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
            board.DoTurn(players[0], distance);

            return players[0].PlayerLocation.SpaceNumber;
        }

        [Test]
        public void Location_Move_Forward_Multiple_Times_Correct_Distance()
        {
            board.DoTurn(players[0], 5);
            board.DoTurn(players[0], 10);
            board.DoTurn(players[0], 20);
            board.DoTurn(players[0], 10);

            Assert.AreEqual(5, players[0].PlayerLocation.SpaceNumber);
        }
    }
}
