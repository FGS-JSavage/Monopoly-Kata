using System;
using System.CodeDom;
using System.Collections.Generic;
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
        private IRealtor realtor;
        private IPlayer player;
        private IJailer jailer;
        private Mock<Dice> mockDice;
        private Mock<Deck> mockDeck;

        [SetUp]
        public void Init()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            mockDice = fixture.Create<Mock<Dice>>();
            //mockRealtor = fixture.Create<Mock<Realtor>>();

            IKernel ninject = new StandardKernel(new BindingsModule());

            ninject.Rebind<IPlayer>().To<Player>().WithConstructorArgument(new GoLocation());
            ninject.Rebind<IDice>().ToConstant(mockDice.Object);
            ninject.Rebind<IDeck>().ToConstant(mockDeck.Object);

            turnHandler = ninject.Get<ITurnHandler>();
            player = ninject.Get<IPlayer>();
            realtor = ninject.Get<IRealtor>();
            //ninject.Get<ILocationFactory>().InjectDecks(ninject.Get<IDeckFactory>().BuildChanceDeck(), ninject.Get<IDeckFactory>().BuildChanceDeck());
            jailer = ninject.Get<IJailer>();
            taskHandler = ninject.Get<ITaskHandler>();
        }

        // ---------------  Release 1 ----------------------------------------------------
        // ---------------  Release 2 ----------------------------------------------------
        // ---------------  Release 3 ----------------------------------------------------
        // ---------------  Release 4 ----------------------------------------------------
        // ---------------  Release 5 ----------------------------------------------------

        [Test]
        public void LandOnChest_DrawAdvanceToGo_PlayerLandsOnGoAndCollects200()
        {
            double initialBalance = player.Balance;
            int landOnGoReward = 200;

            mockDice.Setup(x => x.Score).Returns(3);
            mockDice.Setup(x => x.WasDoubles).Returns(false);

            mockDeck.Setup(x => x.Draw()).Returns(new Card("Advance To Go", new MoveToLocationTask(0, taskHandler)));

            turnHandler.DoTurn(player);

            Assert.AreEqual(initialBalance + landOnGoReward, player.Balance);
            Assert.True(player.PlayerLocation.GetType() == typeof(GoLocation));
        }



    }
}
