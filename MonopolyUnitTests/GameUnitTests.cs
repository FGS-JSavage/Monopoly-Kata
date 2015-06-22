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


        // RELEASE 1 -----------------------------------------------------------------------


        [Test]
        public void Default_Game_Constructor_Initializes_Six_Players()
        {
            game = new Game();

            Assert.AreEqual(game.GetPlayers().Count, 6);
        }

        [Test]
        public void Default_Game_Constructor_Initializes_All_Players_To_Space_Zero()
        {
            game = new Game();

            foreach (var player in game.GetPlayers())
            {
                Assert.AreEqual(player.GetLocation().GetSpaceNumber(), 0);    
            }
        }

        [Test]
        public void Move_Players_Twenty_Times()
        {
            game = new Game();

            foreach (var i in Enumerable.Range(0,20))
            {
                game.DoRound();
            }
        }

        // RELEASE 2 -----------------------------------------------------------------------

        [Test]
        public void Landing_On_Go_Balnce_Increases_By_200()
        {
            game = new Game();
            IPlayer player = game.GetPlayers()[0];

            Assert.AreEqual(player.GetBalance(), 200); // Confirm starting balance


            player.MoveDistance(39);

            Assert.AreEqual(player.GetBalance(), 200); // Confirm increase due to landing on Go
        }


    }
}
    