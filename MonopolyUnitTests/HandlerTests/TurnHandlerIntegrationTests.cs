using System;
using Monopoly.Board;
using Monopoly.Cards;
using Monopoly.Handlers;
using Monopoly.Ninject;
using Monopoly.Player;
using Moq;
using Ninject;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace MonopolyUnitTests.HandlerTests
{
    [TestFixture]
    class TurnHandlerTests : IDisposable
    {
        private IKernel ninject;
        private IFixture fixture;
        private TurnHandler turnHandler;
        private IRealtor realtor;
        private IPlayer player1;
        private IPlayer player2;
        private IJailer jailer;
        private Mock<Dice> mockDice;


        [SetUp]
        public void Init()
        {
            fixture = new Fixture().Customize(new AutoMoqCustomization());

            mockDice = fixture.Create<Mock<Dice>>();

            ninject = new StandardKernel(new BindingsModule());

            ninject.Rebind<IDice>().ToConstant(mockDice.Object);

            turnHandler = ninject.Get<TurnHandler>();
            player1 = ninject.Get<IPlayer>();
            player2 = ninject.Get<IPlayer>();
            realtor = ninject.Get<IRealtor>();
            jailer = ninject.Get<IJailer>();
        }

        [TearDown]
        public void Dispose()
        {
            ninject.Dispose();
        }

        // ---------------  Release 3 ----------------------------------------------------
        [Test]
        public void PlayerLandsOnOwnedRentableSpace_PaysCorrectRent()
        {
            var expectedRent = 2;

            mockDice.Setup(x => x.Score).Returns(1);
            mockDice.Setup(x => x.WasDoubles).Returns(false);
            
            realtor.SetOwnerForSpace(player2, 1);

            double startingBalance = player1.Balance;

            turnHandler.DoTurn(player1);

            Assert.AreEqual(startingBalance - expectedRent, player1.Balance);
            Assert.AreEqual(startingBalance + expectedRent, player2.Balance);
        }

        [Test]
        public void PlayerLandingOnAnUnownedSpace_AutomaticallyBuysIt()
        {
            mockDice.Setup(x => x.Score).Returns(1);
            mockDice.Setup(x => x.WasDoubles).Returns(false);

            double startingBalance = player1.Balance;

            turnHandler.DoTurn(player1);

            Assert.AreEqual(startingBalance - 60, player1.Balance);
            Assert.AreSame(player1, realtor.GetOwnerForSpace(1));
        }

        [Test]
        public void PlayerLandingOnAPropertyThatHeOwns_BalanceIsUnchanged()
        {
            mockDice.Setup(x => x.Score).Returns(1);
            mockDice.Setup(x => x.WasDoubles).Returns(false);

            realtor.SetOwnerForSpace(player1, 1);

            double startingBalance = player1.Balance;

            turnHandler.DoTurn(player1);

            Assert.AreEqual(startingBalance, player1.Balance);
            Assert.AreSame(player1, realtor.GetOwnerForSpace(1));
        }

        // ---------------  Release 4 ----------------------------------------------------

        [Test]
        public void PlayerRollsNonDoublesLandingOnGoToJail_TurnIsOverAndBalanceIsUnchanged()
        {
            mockDice.Setup(x => x.Score).Returns(30);
            mockDice.Setup(x => x.WasDoubles).Returns(false);

            double startingBalance = player1.Balance;

            turnHandler.DoTurn(player1);

            Assert.AreEqual(startingBalance, player1.Balance);
            Assert.IsTrue(jailer.PlayerIsImprisoned(player1));
        }

        [Test]
        public void PlayerRollsDoublesLandingOnGoToJail_TurnIsOverAndBalanceIsUnchanged()
        {
            mockDice.Setup(x => x.Score).Returns(30);
            mockDice.Setup(x => x.WasDoubles).Returns(true);

            double startingBalance = player1.Balance;

            turnHandler.DoTurn(player1);

            Assert.AreEqual(startingBalance, player1.Balance);
            Assert.IsTrue(jailer.PlayerIsImprisoned(player1));
        }

        [Test]
        public void PlayerPassesOverGoToJail_PlayerIsNotInJail()
        {
            mockDice.Setup(x => x.Score).Returns(28);
            mockDice.Setup(x => x.WasDoubles).Returns(false);

            turnHandler.DoTurn(player1);

            mockDice.Setup(x => x.Score).Returns(4);

            Assert.IsFalse(jailer.PlayerIsImprisoned(player1));
        }

        [Test]
        public void RollDoubles3TimesInARow_PlayerIsInJail()
        {
            mockDice.Setup(x => x.WasDoubles).Returns(true);

            for (int i = 0; i < 3; i++)
            {
                turnHandler.DoTurn(player1);
            }

            Assert.IsTrue(jailer.PlayerIsImprisoned(player1));
        }

        [Test]
        public void RollDoubles2TimesInARow_PlayerIsNotInJail()
        {
            mockDice.Setup(x => x.WasDoubles).Returns(true);

            for (int i = 0; i < 2; i++)
            {
                turnHandler.DoTurn(player1);
            }

            Assert.IsFalse(jailer.PlayerIsImprisoned(player1));
        }

        [Test]
        public void PlayerIsInJail_PaysForFreedomRollsDoublesMovesRollsAgain_BalanceIsDecreasedBy50()
        {
            double initialBalance = player1.Balance;
            int JailFee = 50;

            mockDice.Setup(x => x.Score).Returns(10);
            mockDice.Setup(x => x.WasDoubles).Returns(true);
            player1.PreferedJailStrategy = JailStrategy.Pay;
            turnHandler.SendPlayerToJail(player1);

            turnHandler.DoTurn(player1);

            mockDice.Setup(x => x.Score).Returns(0);
            turnHandler.DoTurn(player1);

            Assert.AreEqual(initialBalance - JailFee, player1.Balance);
        }

        [Test]
        public void PlayerIsInJailWithNoGetOutFreeCard_PrefersToUseCardButDefaultsToRollDoubles_PlayerRollsDoubles_PlayerIsNotInAJailAndBalanceIsUnchanged()
        {
            double initialBalance = player1.Balance;

            mockDice.Setup(x => x.Score).Returns(10);
            mockDice.Setup(x => x.WasDoubles).Returns(true);
            player1.PreferedJailStrategy = JailStrategy.UseGetOutOfJailCard;
            turnHandler.SendPlayerToJail(player1);

            turnHandler.DoTurn(player1);

            mockDice.Setup(x => x.Score).Returns(0);
            turnHandler.DoTurn(player1);

            Assert.AreEqual(initialBalance, player1.Balance);
            Assert.False(jailer.PlayerIsImprisoned(player1));
        }

        [Test]
        public void PlayerIsInJail_PaysForFreedomRollsDoublesMoves_BalanceIsDecreasedBy50()
        {
            double initialBalance = player1.Balance;
            var JailFee = 50;

            mockDice.Setup(x => x.Score).Returns(10);
            mockDice.Setup(x => x.WasDoubles).Returns(true);
            player1.PreferedJailStrategy = JailStrategy.Pay;
            turnHandler.SendPlayerToJail(player1);

            turnHandler.DoTurn(player1);

            Assert.AreEqual(initialBalance - JailFee, player1.Balance);
        }

        [Test]
        public void PlayerIsInJailUsingRollsForFreedomStrategy_RollsDoublesMoveOnce_TurnIsOver()
        {
            double initialBalance = player1.Balance;
            var initialSpaceNumber = 10;
            var expectedDistance = 10;

            mockDice.Setup(x => x.Score).Returns(expectedDistance);
            mockDice.Setup(x => x.WasDoubles).Returns(true);
            player1.PreferedJailStrategy = JailStrategy.RollDoubles;
            turnHandler.SendPlayerToJail(player1);

            turnHandler.DoTurn(player1);

            Assert.AreEqual(initialBalance, player1.Balance); 
            Assert.AreEqual(initialSpaceNumber + expectedDistance, player1.PlayerLocation.SpaceNumber);
        }

        [Test]
        public void PlayerIsInJailUsingRollsForFreedomStrategy_RollsNonDoublesTwice_StillInJail()
        {
            mockDice.Setup(x => x.WasDoubles).Returns(false);
            player1.PreferedJailStrategy = JailStrategy.RollDoubles;
            turnHandler.SendPlayerToJail(player1);

            turnHandler.DoTurn(player1);
            turnHandler.DoTurn(player1);
            
            Assert.IsTrue(jailer.PlayerIsImprisoned(player1));
        }

        [Test]
        public void PlayerIsInJailUsingRollsForFreedomStrategy_RollsDoublesOnThirdTry_MovesAndTurnIsOver()
        {
            double initialBalance = player1.Balance;
            var initialSpaceNumber = 10;
            var expectedDistance = 10;

            mockDice.Setup(x => x.Score).Returns(expectedDistance);
            mockDice.Setup(x => x.WasDoubles).Returns(false);
            player1.PreferedJailStrategy = JailStrategy.RollDoubles;
            turnHandler.SendPlayerToJail(player1);

            turnHandler.DoTurn(player1);
            turnHandler.DoTurn(player1);

            mockDice.Setup(x => x.WasDoubles).Returns(true);

            turnHandler.DoTurn(player1);

            Assert.AreEqual(initialBalance, player1.Balance);
            Assert.AreEqual(initialSpaceNumber + expectedDistance, player1.PlayerLocation.SpaceNumber);
        }

        [Test]
        public void PlayerIsInJailUsingRollsForFreedomStrategy_RollsNonDoublesThreeTimes_PaysToGetOutOfJailAndMoves()
        {
            double initialBalance = player1.Balance;
            var initialSpaceNumber = 10;
            var expectedDistance = 10;
            var jailFee = 50;

            mockDice.Setup(x => x.Score).Returns(expectedDistance);
            mockDice.Setup(x => x.WasDoubles).Returns(false);
            player1.PreferedJailStrategy = JailStrategy.RollDoubles;
            turnHandler.SendPlayerToJail(player1);

            turnHandler.DoTurn(player1);
            turnHandler.DoTurn(player1);
            turnHandler.DoTurn(player1);
            
            Assert.AreEqual(initialBalance - jailFee, player1.Balance);
            Assert.AreEqual(initialSpaceNumber + expectedDistance, player1.PlayerLocation.SpaceNumber);
        }

        // ---------------  Release 5 ----------------------------------------------------

        [Test]
        public void PlayerIsInJail_UsesGetOutOfJailCard_PlayerMovesAndCardIsReturnedToBottomOfStack()
        {
            var jailPosition = 10;
            var rollDistance = 10;

            mockDice.Setup(x => x.Score).Returns(rollDistance);
            mockDice.Setup(x => x.WasDoubles).Returns(false);

            var mockCard = fixture.Create<Mock<GetOutOfJailCard>>();
            mockCard.Setup(x => x.Type).Returns(DeckType.Chance);

            player1.AddGetOutOfJailCard(mockCard.Object);
            player1.PreferedJailStrategy = JailStrategy.UseGetOutOfJailCard;

            turnHandler.SendPlayerToJail(player1);

            turnHandler.DoTurn(player1);

            Assert.False(player1.HasGetOutOfJailCard());
            Assert.AreEqual(jailPosition + rollDistance, player1.PlayerLocation.SpaceNumber);
        }
    }
}
