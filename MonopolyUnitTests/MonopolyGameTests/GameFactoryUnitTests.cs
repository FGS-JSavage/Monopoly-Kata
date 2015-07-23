using Monopoly;
using Monopoly.MonopolyGame;
using Monopoly.Player;
using NUnit.Framework;

namespace MonopolyUnitTests.MonopolyGameTests
{
    [TestFixture]
    class GameFactoryUnitTests
    {
        private Game game;
        private GameFactory gameFactory;

        [SetUp]
        public void Init()
        {
            gameFactory = new GameFactory();
        }

        // ---------------  Release 1 ----------------------------------------------------

        [Test]
        public void CreateAGameWithLessThan2PlayersFails()
        {
            game = gameFactory.BuildGame(1);

            Assert.IsNull(game);

            game = gameFactory.BuildGame("p1");

            Assert.IsNull(game);
        }

        [Test]
        public void CreateAGameWithMoreThan8PlayersFails()
        {
            game = gameFactory.BuildGame(9);

            Assert.IsNull(game);

            game = gameFactory.BuildGame("p1", "p2", "p3", "p4", "p5", "p6", "p7", "p8", "p9");

            Assert.IsNull(game);
        }

        [Test]
        public void CreateGame_PlayersShouldBeInRandomOrder()
        {

            string[] names = new string[] {"amy", "bill"};

            game = gameFactory.BuildGame(names);

            string nameOfFirstRoller = null;
            string nameOfLastRoundsFirstRoller = null;

            bool orderHasSwapped = false;

            for (int i = 0; i < 100 ^ orderHasSwapped; i++)
            {
                nameOfLastRoundsFirstRoller = nameOfFirstRoller;

                game = gameFactory.BuildGame(names);

                nameOfFirstRoller = game.GetPlayers()[0].Name;

                orderHasSwapped = nameOfFirstRoller != nameOfLastRoundsFirstRoller && i > 2;
            }

            Assert.True(orderHasSwapped);
        }

        [Test]
        public void Play20Rounds_EveryPlayerShouldHaveTaken20Turns()
        {
            game = gameFactory.BuildGame(5);

            for (int i = 0; i < 20; i++)
            {
                game.DoRound();
            }

            foreach(IPlayer player in game.GetPlayers())
            {
                Assert.AreEqual(20, player.RoundsPlayed);    
            }
        }

        // ---------------  Release 2 ----------------------------------------------------
        // ---------------  Release 3 ----------------------------------------------------
        // ---------------  Release 4 ----------------------------------------------------
        // ---------------  Release 5 ----------------------------------------------------
    }
}
    