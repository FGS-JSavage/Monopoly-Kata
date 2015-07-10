using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Monopoly;
using Monopoly.Ninject;
using Moq;
using Ninject;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace MonopolyUnitTests
{
    [TestFixture]
    class TurnHandlerTests
    {
        private ITurnHandler turnHandler;
        //private Mock<Realtor> mockRealtor;
        private IRealtor realtor;
        private IPlayer player;
        private IJailer jailer;
        private Mock<Dice> mockDice;

        [SetUp]
        public void Init()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            mockDice = fixture.Create<Mock<Dice>>();
            //mockRealtor = fixture.Create<Mock<Realtor>>();

            IKernel ninject = new StandardKernel(new BindingsModule());

            ninject.Rebind<IPlayer>().To<Player>().WithConstructorArgument(new GoLocation());
            ninject.Rebind<IDice>().ToConstant(mockDice.Object);
            //ninject.Rebind<IRealtor>().ToConstant(mockRealtor.Object);

            turnHandler = ninject.Get<ITurnHandler>();
            player = ninject.Get<IPlayer>();
            realtor = ninject.Get<IRealtor>();
            jailer = ninject.Get<IJailer>();
        }

        // ---------------  Release 3 ----------------------------------------------------
        [Test]
        public void PlayerLandingOnAnUnownedSpace_AutomaticallyBuysIt()
        {
            mockDice.Setup(x => x.Score).Returns(1);
            mockDice.Setup(x => x.WasDoubles).Returns(false);

            double startingBalance = player.Balance;

            turnHandler.DoTurn(player);

            Assert.AreEqual(startingBalance - 60, player.Balance);
            Assert.AreSame(player, realtor.GetOwnerForSpace(1));
        }

        [Test]
        public void PlayerLandingOnAPropertyThatHeOwns_BalanceIsUnchanged()
        {
            mockDice.Setup(x => x.Score).Returns(1);
            mockDice.Setup(x => x.WasDoubles).Returns(false);

            realtor.SetOwnerForSpace(player, 1);

            double startingBalance = player.Balance;

            turnHandler.DoTurn(player);

            Assert.AreEqual(startingBalance, player.Balance);
            Assert.AreSame(player, realtor.GetOwnerForSpace(1));
        }

        // ---------------  Release 4 ----------------------------------------------------

        [Test]
        public void PlayerRollsNonDoubleslandingOnGoToJail_TurnIsOverAndBalanceIsUnchanged()
        {
            mockDice.Setup(x => x.Score).Returns(30);
            mockDice.Setup(x => x.WasDoubles).Returns(false);

            double startingBalance = player.Balance;

            turnHandler.DoTurn(player);

            Assert.AreEqual(startingBalance, player.Balance);
            Assert.IsTrue(jailer.PlayerIsImprisoned(player));
        }

        [Test]
        public void PlayerRollsDoubleslandingOnGoToJail_TurnIsOverAndBalanceIsUnchanged()
        {
            mockDice.Setup(x => x.Score).Returns(30);
            mockDice.Setup(x => x.WasDoubles).Returns(true);

            double startingBalance = player.Balance;

            turnHandler.DoTurn(player);

            Assert.AreEqual(startingBalance, player.Balance);
            Assert.IsTrue(jailer.PlayerIsImprisoned(player));
        }

        [Test]
        public void PlayerPassesOverGoToJail_PlayerIsNotInJail()
        {
            mockDice.Setup(x => x.Score).Returns(28);
            mockDice.Setup(x => x.WasDoubles).Returns(false);

            turnHandler.DoTurn(player);

            mockDice.Setup(x => x.Score).Returns(4);

            Assert.IsFalse(jailer.PlayerIsImprisoned(player));
        }

        [Test]
        public void RollDoubles3TimesInARow_PlayerIsInJail()
        {
            mockDice.Setup(x => x.WasDoubles).Returns(true);

            double startingBalance = player.Balance;

            for (int i = 0; i < 3; i++)
            {
                turnHandler.DoTurn(player);
            }

            Assert.IsTrue(jailer.PlayerIsImprisoned(player));
        }

        [Test]
        public void RollDoubles2TimesInARow_PlayerIsNotInJail()
        {
            mockDice.Setup(x => x.WasDoubles).Returns(true);

            double startingBalance = player.Balance;

            for (int i = 0; i < 2; i++)
            {
                turnHandler.DoTurn(player);
            }

            Assert.IsFalse(jailer.PlayerIsImprisoned(player));
        }

        // ---------------  Release 5 ----------------------------------------------------
        
    }
}
