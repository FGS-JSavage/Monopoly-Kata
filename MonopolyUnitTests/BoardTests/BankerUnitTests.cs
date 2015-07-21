using System;
using Autofac.Extras.Moq;
using Monopoly;
using Monopoly.Board;
using NUnit.Framework;

namespace MonopolyUnitTests.BoardTests
{
    [TestFixture]
    class BankerUnitTests : IDisposable
    {
        private Banker banker;
        private AutoMock mocker;

        private Player player;

        [SetUp]
        public void Init()
        {
            mocker = AutoMock.GetLoose();
            banker = mocker.Create<Banker>();
            player = mocker.Create<Player>();
        }

        [TearDown]
        public void Dispose()
        {
            mocker.Dispose();
        }

        [Test]
        public void Pay_CorrectlyTransfersFundsBetweenPlayers()
        {
            var transferAmount = 20;
            var playerInitialBalance = player.Balance;

            banker.Payout(player, transferAmount);

            Assert.AreEqual(playerInitialBalance + transferAmount, player.Balance);
        }

        [Test]
        public void Charge_CorrectlyTransfersFundsBetweenPlayers()
        {
            var transferAmount = 20;
            var playerInitialBalance = player.Balance;

            banker.Collect(player, transferAmount);

            Assert.AreEqual(playerInitialBalance - transferAmount, player.Balance);
        }

        [Test]
        public void Transfer_CorrectlyTransfersFundsBetweenPlayers()
        {
            Player recipient = mocker.Create<Player>();
            
            var transferAmount = 20;
            var playerInitialBalance = player.Balance;
            var recipientoInitialBalance = recipient.Balance;

            banker.Transfer(player, recipient, transferAmount);

            Assert.AreEqual(recipientoInitialBalance + transferAmount, recipient.Balance);
            Assert.AreEqual(playerInitialBalance - transferAmount, player.Balance);
        }
    }
}
