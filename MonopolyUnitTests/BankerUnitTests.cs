using Autofac.Extras.Moq;
using Monopoly;
using NUnit.Framework;

namespace MonopolyUnitTests
{
    [TestFixture]
    class BankerUnitTests
    {
        private Banker banker;
        AutoMock mocker;

        [SetUp]
        public void Init()
        {
            mocker = AutoMock.GetLoose();

            banker = mocker.Create<Banker>();
        }

        [Test]
        public void Pay_CorrectlyTransfersFundsBetweenPlayers()
        {
            Player player = mocker.Create<Player>();

            int transferAmount = 20;
            var playerInitialBalance = player.Balance;

            banker.Payout(player, transferAmount);

            Assert.AreEqual(playerInitialBalance + transferAmount, player.Balance);
        }

        [Test]
        public void Charge_CorrectlyTransfersFundsBetweenPlayers()
        {
            Player player = mocker.Create<Player>();

            int transferAmount = 20;
            var playerInitialBalance = player.Balance;

            banker.Collect(player, transferAmount);

            Assert.AreEqual(playerInitialBalance - transferAmount, player.Balance);
        }

        [Test]
        public void Transfer_CorrectlyTransfersFundsBetweenPlayers()
        {
            Player payer =     mocker.Create<Player>();
            Player recipient = mocker.Create<Player>();
            
            int transferAmount = 20;
            var payerInitialBalance = payer.Balance;
            var recipientoInitialBalance = recipient.Balance;

            banker.Transfer(payer, recipient, transferAmount);

            Assert.AreEqual(recipientoInitialBalance + transferAmount, recipient.Balance);
            Assert.AreEqual(recipientoInitialBalance - transferAmount, payer.Balance);
        }
    }
}
