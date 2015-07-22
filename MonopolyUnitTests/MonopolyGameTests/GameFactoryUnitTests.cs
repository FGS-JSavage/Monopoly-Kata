using Monopoly;
using Monopoly.MonopolyGame;
using Monopoly.Ninject;
using Ninject;
using NUnit.Framework;

namespace MonopolyUnitTests.MonopolyGameTests
{
    [TestFixture]
    class GameFactoryUnitTests
    {
        private IKernel ninject;
        private Game game;


        [SetUp]
        public void Init()
        {
            ninject = new StandardKernel(new BindingsModule());
            game = ninject.Get<Game>();
        }

        [TearDown]
        public void TearDown()
        {
            ninject.Dispose();
        }

        // ---------------  Release 1 ----------------------------------------------------

        [Test]
        public void CreateAGameWithLessThan2PlayersFails()
        {
            game = GameFactory.BuildGame(1);

            Assert.IsNull(game);

            game = GameFactory.BuildGame("p1");

            Assert.IsNull(game);
        }

        [Test]
        public void CreateAGameWithMoreThan8PlayersFails()
        {
            game = GameFactory.BuildGame(9);

            Assert.IsNull(game);

            game = GameFactory.BuildGame("p1", "p2", "p3", "p4", "p5", "p6", "p7", "p8", "p9");

            Assert.IsNull(game);
        }

        [Test]
        public void CreateGame_PlayersShouldBeInRandomOrder()
        {

            string[] names = new string[] {"amy", "bill"};

            game = GameFactory.BuildGame(names);

            string nameOfFirstRoller = null;
            string nameOfLastRoundsFirstRoller = null;

            bool orderHasSwapped = false;

            for (int i = 0; i < 100 ^ orderHasSwapped; i++)
            {
                nameOfLastRoundsFirstRoller = nameOfFirstRoller;

                game = GameFactory.BuildGame(names);

                nameOfFirstRoller = game.GetPlayers()[0].Name;

                orderHasSwapped = nameOfFirstRoller != nameOfLastRoundsFirstRoller && i > 2;
            }

            Assert.True(orderHasSwapped);
        }

        [Test]
        public void Play20Rounds_EveryPlayerShouldHaveTaken20Turns()
        {
            game = GameFactory.BuildGame(5);

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
    