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
    class GameUnitTests
    {
        private IGame game;

        [SetUp]
        public void Init()
        {
            game = new Game();
        }

        // RELEASE 1 -----------------------------------------------------------------------

        [Test]
        public void Default_Game_Constructor_Initializes_Six_Players()
        {
            Assert.AreEqual(game.GetPlayers().Count, 6);
        }

        [Test]
        public void Default_Game_Constructor_Initializes_All_Players_To_Space_Zero()
        {
            foreach (var player in game.GetPlayers())
            {
                Assert.AreEqual(player.PlayerLocation.GetSpaceNumber(), 0);    
            }
        }

        [Test]
        public void Move_Players_Twenty_Times()
        {
            foreach (var i in Enumerable.Range(0,20))
            {
                game.DoRound();
            }
        }

        // RELEASE 2 -----------------------------------------------------------------------

        [Test]
        public void Landing_On_Go_Balnce_Increases_By_200()
        {
            IPlayer player = game.GetPlayers()[0];

            Assert.AreEqual(0, player.Balance); // Confirm starting balance

            player.MoveDistance(40);

            Assert.AreEqual(200, player.Balance); // Confirm increase due to landing on Go
        }

        [Test]
        public void Landing_On_A_Normal_Location_Does_Not_Change_Ballance()
        {
            IPlayer player = game.GetPlayers()[0];

            Assert.AreEqual(0, player.Balance); // Confirm starting balance

            player.MoveDistance(5);

            Assert.AreEqual(0, player.Balance); // Confirm increase due to landing on Go
        }


    }
}
    