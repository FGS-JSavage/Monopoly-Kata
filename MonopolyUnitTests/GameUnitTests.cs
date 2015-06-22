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

    }
}
    