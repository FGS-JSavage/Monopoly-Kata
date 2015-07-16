using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly;
using Monopoly.Locations;
using Monopoly.Ninject;
using Monopoly.Tasks;
using Moq;
using Ninject;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;


namespace MonopolyUnitTests
{
    [TestFixture]
    class CardIntegrationTests
    {
        private ITurnHandler turnHandler;
        private ITaskHandler taskHandler;
        private IDeckFactory deckFactory;
        private IRealtor realtor;
        private IPlayer player;
        private IJailer jailer;
        private Mock<Dice> mockDice;
        private Mock<IDeck> mockDeck;
        private Mock<IDeckFactory> mockDeckFactory;
        private Mock<IPlayer> mockPlayer;


        [SetUp]
        public void Init()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            mockDeck = fixture.Create<Mock<IDeck>>();
            mockDice = fixture.Create<Mock<Dice>>();
            mockDeckFactory = fixture.Create<Mock<IDeckFactory>>();
            mockPlayer = fixture.Create<Mock<IPlayer>>();

            IKernel ninject = new StandardKernel(new BindingsModule());

            ninject.Rebind<IDice>().ToConstant(mockDice.Object).InSingletonScope();
            ninject.Rebind<IDeckFactory>().ToConstant(mockDeckFactory.Object).InSingletonScope();
            ninject.Rebind<ITaskHandler>().To<TaskHandler>().WithConstructorArgument(PlayerFactory.BuildPlayers(6)); // register six OTHER players
            

            mockDeckFactory.Setup(x => x.BuildChanceDeck()).Returns(mockDeck.Object);
            mockDeckFactory.Setup(x => x.BuildCommunitiyChestDeck()).Returns(mockDeck.Object);

            IDeckFactory deckFactory = ninject.Get<IDeckFactory>();

            ILocationFactory d = ninject.Get<ILocationFactory>();

            turnHandler = ninject.Get<ITurnHandler>();
            player = ninject.Get<IPlayer>();
            realtor = ninject.Get<IRealtor>();

            jailer = ninject.Get<IJailer>();
            taskHandler = ninject.Get<ITaskHandler>();
        }

        // ---------------  Release 1 ----------------------------------------------------
        // ---------------  Release 2 ----------------------------------------------------
        // ---------------  Release 3 ----------------------------------------------------
        // ---------------  Release 4 ----------------------------------------------------
        // ---------------  Release 5 ----------------------------------------------------


        [Test]
        public void DrawMoveToLocationTask_PlayerLandsOnGoAndCollects200()
        {
            double initialBalance = player.Balance;
            int landOnGoReward = 200;

            mockDice.Setup(x => x.Score).Returns(2);
            mockDice.Setup(x => x.WasDoubles).Returns(false);

            mockDeck.Setup(x => x.Draw()).Returns(new Card("Advance To Go", new MoveToLocationTask(0, taskHandler), DeckType.Chance));

            turnHandler.DoTurn(player);

            Assert.AreEqual(initialBalance + landOnGoReward, player.Balance);
            Assert.True(player.PlayerLocation.GetType() == typeof(GoLocation));
        }

        [Test]
        public void DrawCollectFromBanker_PlayerBalanceIsUpdatedCorrectly()
        {
            double initialBalance = player.Balance;
            int amount = 50;

            mockDice.Setup(x => x.Score).Returns(2);
            mockDice.Setup(x => x.WasDoubles).Returns(false);

            mockDeck.Setup(x => x.Draw()).Returns(new Card("Bank Error In Your Favor", new CollectFromBankerTask(amount, taskHandler), DeckType.Chance));

            turnHandler.DoTurn(player);

            Assert.AreEqual(initialBalance + amount, player.Balance);
        }

        [Test]
        public void DrawPayBanker_PlayerBalanceIsUpdatedCorrectly()
        {
            double initialBalance = player.Balance;
            int amount = 50;

            mockDice.Setup(x => x.Score).Returns(2);
            mockDice.Setup(x => x.WasDoubles).Returns(false);

            mockDeck.Setup(x => x.Draw()).Returns(new Card("card name", new PayBankerTask(amount, taskHandler), DeckType.Chance));

            turnHandler.DoTurn(player);

            Assert.AreEqual(initialBalance - amount, player.Balance);
        }

        [Test]
        public void DrawGoDirectlyToJail_PlayerBalanceAndLocationAreUpdatedCorrectly()
        {
            double initialBalance = player.Balance;
            int amount = 50;

            mockDice.Setup(x => x.Score).Returns(2);
            mockDice.Setup(x => x.WasDoubles).Returns(false);

            mockDeck.Setup(x => x.Draw()).Returns(new Card("card name", new GoDirectlyToJailTask(taskHandler), DeckType.Chance));

            turnHandler.DoTurn(player);

            Assert.AreEqual(initialBalance, player.Balance);
            Assert.True(player.PlayerLocation.GetType() == typeof(JailLocation));
        }


        [Test]
        public void DrawCollectFromAll_BalanceIsUpdatedCorrectly()
        {
            double initialBalance = player.Balance;
            int amount = 10;
            int numberOfPlayers = 6;

            mockDice.Setup(x => x.Score).Returns(2);
            mockDice.Setup(x => x.WasDoubles).Returns(false);

            
            mockDeck.Setup(x => x.Draw()).Returns(new Card("card name", new CollectFromAllTask(10, taskHandler), DeckType.Chance));

            turnHandler.DoTurn(player);

            Assert.AreEqual(initialBalance + amount * numberOfPlayers, player.Balance);
        }

        [Test]
        public void DrawMoveDistanceTask_LocationIsUpdatedCorrectly()
        {
            int expectedPosition = 12;
            int distance = 10;

            mockDice.Setup(x => x.Score).Returns(2);
            mockDice.Setup(x => x.WasDoubles).Returns(false);


            mockDeck.Setup(x => x.Draw()).Returns(new Card("card name", new MoveDistanceTask(distance, taskHandler), DeckType.Chance));

            turnHandler.DoTurn(player);

            Assert.AreEqual(expectedPosition, player.PlayerLocation.SpaceNumber);
        }

        [Test]
        public void DrawMoveBackThreeSpacesDistance_LocationIsUpdatedCorrectly()
        {
            int distance = -5;
            int expectedPosition = 37;


            mockDice.Setup(x => x.Score).Returns(2);
            mockDice.Setup(x => x.WasDoubles).Returns(false);


            mockDeck.Setup(x => x.Draw()).Returns(new Card("card name", new MoveDistanceTask(distance, taskHandler), DeckType.Chance));

            turnHandler.DoTurn(player);

            Assert.AreEqual(expectedPosition, player.PlayerLocation.SpaceNumber);
        }

        [Test]
        public void DrawPayAllPlayers_BalanceIsUpdatedCorrectly()
        {
            double initialBalance = player.Balance;
            int amount = 50;
            int numberOfPlayers = 6;

            mockDice.Setup(x => x.Score).Returns(2);
            mockDice.Setup(x => x.WasDoubles).Returns(false);

            mockDeck.Setup(x => x.Draw()).Returns(new Card("card name", new PayAllOtherPlayersTask(amount, taskHandler), DeckType.Chance));

            turnHandler.DoTurn(player);

            Assert.AreEqual(initialBalance - amount * numberOfPlayers, player.Balance);
        }

        [Test]
        public void DrawGetOutOfJailFreeCard_GoToJail_UseGetOutOfJailFreeCard_PlayerIsNotInJail()
        {
            double initialBalance = player.Balance;
            int amount = 50;
            int numberOfPlayers = 6;

            mockDice.Setup(x => x.Score).Returns(2);
            mockDice.Setup(x => x.WasDoubles).Returns(false);

            mockDeck.Setup(x => x.Draw()).Returns(new Card("card name", new GetOutOfJailTask(taskHandler), DeckType.Chance));

            turnHandler.DoTurn(player);

            Assert.False(jailer.PlayerIsImprisoned(player));
        }

        [Test]
        [TestCase( 7, Result = 12)]
        [TestCase(22, Result = 28)]
        [TestCase(36, Result = 28)]
        public int DrawMoveToClosestUtility_MovesPlayerToTheCorrectLocation(int chanceSpaceNumber)
        {        
            mockDice.Setup(x => x.Score).Returns(chanceSpaceNumber);
            mockDice.Setup(x => x.WasDoubles).Returns(false);

            mockDeck.Setup(x => x.Draw()).Returns(new Card("card name", new MoveToNearestUtilityTask(taskHandler), DeckType.Chest));

            turnHandler.DoTurn(player);

            return player.PlayerLocation.SpaceNumber;
        }

        [Test]
        [TestCase( 7, Result =  5)]
        [TestCase(22, Result = 25)]
        [TestCase(36, Result = 35)]
        public int DrawMoveToClosestRailroad_MovesPlayerToTheCorrectLocation(int chanceSpaceNumber)
        {
            mockDice.Setup(x => x.Score).Returns(chanceSpaceNumber);
            mockDice.Setup(x => x.WasDoubles).Returns(false);

            mockDeck.Setup(x => x.Draw()).Returns(new Card("card name", new MoveToNearestRailroadTask(taskHandler), DeckType.Chance));

            turnHandler.DoTurn(player);

            return player.PlayerLocation.SpaceNumber;
        }

        [Test]
        public void PlayerDrawsMoveToClosestUtility_UtilityIsOwned_PlayerRollsDiceAndIsCharged10xTheRollValue()
        {
            double inititialBalance = player.Balance;
            int rollAmount = 10;
            int utilitySpaceNumber = 12;

            realtor.SetOwnerForSpace(mockPlayer.Object, utilitySpaceNumber);

            mockDice.Setup(x => x.Score).Returns(new Queue<int>(new[] { 2, rollAmount }).Dequeue);
            mockDice.Setup(x => x.WasDoubles).Returns(false);

            mockDeck.Setup(x => x.Draw()).Returns(new Card("card name", new MoveToNearestUtilityTask(taskHandler), DeckType.Chance));

            turnHandler.DoTurn(player);

            Assert.AreEqual(inititialBalance - 10 * rollAmount, player.Balance);
        }

        [Test]
        public void PlayerDrawsMoveToNearestRailroad_RailroadIsOwned_PlayerPaysDoubleRent()
        {
            double inititialBalance = player.Balance;
            int railroadSpaceNumber = 5;
            int normalRent = 25;

            realtor.SetOwnerForSpace(mockPlayer.Object, railroadSpaceNumber);

            mockDice.Setup(x => x.Score).Returns(2);
            mockDice.Setup(x => x.WasDoubles).Returns(false);

            mockDeck.Setup(x => x.Draw()).Returns(new Card("card name", new MoveToNearestRailroadTask(taskHandler), DeckType.Chance));

            turnHandler.DoTurn(player);

            Assert.AreEqual(inititialBalance - 2 * normalRent, player.Balance);
        }


    }
}
