using Monopoly;
using Monopoly.Handlers;
using Monopoly.Ninject;
using Ninject;
using NUnit.Framework;

namespace MonopolyUnitTests.HandlerTests
{
    [TestFixture]
    class MovementHandlerUnitTests
    {
        private IMovementHandler movementHandler;
        private IPlayer player;

        [SetUp]
        public void Init()
        {
            IKernel ninject = new StandardKernel(new BindingsModule());

            movementHandler = ninject.Get<IMovementHandler>();

            player = ninject.Get<IPlayer>();
        }

        // ---------------  Release 1 ----------------------------------------------------

        [Test]
        public void StartAtGo_RollSeven_MovesToSeven()
        {
            int startingSpace = player.PlayerLocation.SpaceNumber;
            int rollValue = 7;

            movementHandler.MovePlayer(player, rollValue);

            Assert.AreEqual(rollValue + startingSpace, player.PlayerLocation.SpaceNumber);
        }

        [Test]
        public void PlayerOnLocation39_Rolls6_EndsUpOn5()
        {
            int rollValue = 6;
            int startingSpace = 39;
            int expectedLandingSpace = 5;

            movementHandler.MovePlayerDirectlyToSpaceNumber(player, 39);

            Assert.AreEqual(startingSpace, player.PlayerLocation.SpaceNumber);

            movementHandler.MovePlayer(player, rollValue);

            Assert.AreEqual(expectedLandingSpace, player.PlayerLocation.SpaceNumber);
        }

        // ---------------  Release 2 ----------------------------------------------------

        [Test]
        public void PlayerLandsOnGo_BalanceIncreasesBy200()
        {
            double initialBalance = player.Balance;

            movementHandler.MovePlayerDirectlyToSpaceNumber(player, 0);

            Assert.AreEqual(initialBalance + 200, player.Balance);
        }

        [Test]
        public void PlayerLandsOnNormalLocation_BalanceDoesNotChange()
        {
            double initialBalance = player.Balance;

            movementHandler.MovePlayerDirectlyToSpaceNumber(player, 20);

            Assert.AreEqual(initialBalance, player.Balance);            
        }

        [Test]
        public void PassingOverGo_IncreasesBalanceBy200()
        {
            double initialBalance = player.Balance;

            movementHandler.MovePlayer(player, 60);

            Assert.AreEqual(initialBalance + 200, player.Balance);
        }

        [Test]
        public void PassingOverGoTwiceInOneTurn_IncreasesBalanceBy400()
        {
            double initialBalance = player.Balance;

            movementHandler.MovePlayer(player, 100);

            Assert.AreEqual(initialBalance + 400, player.Balance);
        }

        [Test]
        [TestCase(1800, Result = 1620.0)]
        [TestCase(2200, Result = 2000.0)]
        [TestCase(0, Result = 0.0)]
        [TestCase(2000, Result = 1800.0)]
        public double Land_On_Income_Tax_Charges_Correctly(int startingBalance)
        {
            player.Balance = startingBalance; 

            movementHandler.MovePlayer(player, 4);

            return player.Balance;
        }

        [Test]
        [TestCase(1000, Result = 925.0)]
        [TestCase(75, Result = 0.0)]
        [TestCase(50, Result = 0.0)]
        [TestCase(0, Result = 0.0)]
        public double Landing_On_Luxury_Tax_Decreases_Balance_By_75(int startingBalance)
        {

            player.Balance = startingBalance; 

            movementHandler.MovePlayer(player, 38);

            return player.Balance;
        }

        [Test]
        public void PassOverAUnownedLocation_BalanceIsUnchanged()
        {
            double initialBalance = player.Balance;

            movementHandler.MovePlayer(player, 20);

            Assert.AreEqual(initialBalance, player.Balance);
        }

        // ---------------  Release 4 ----------------------------------------------------
        // ---------------  Release 5 ----------------------------------------------------

        [Test]
        public void PlayerPassesOverCommunityChest_NothingHappens()
        {
            double initialBalance = player.Balance;

            movementHandler.MovePlayer(player, 10);

            Assert.AreEqual(initialBalance, player.Balance);
        }
    }
}
