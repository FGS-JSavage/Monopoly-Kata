using Monopoly.Board.Locations;
using Monopoly.Cards;
using Monopoly.Player;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace MonopolyUnitTests.PlayerTests
{

    [TestFixture]
    public class PlayerUnitTests
    {
        private IPlayer player;
        private ILocation startingLocation;
        private Mock<Card> mockCard;

        [SetUp]
        public void Init()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            mockCard = fixture.Create<Mock<Card>>();

            startingLocation = new GoLocation();
            player = new Player(startingLocation);
        }

        [Test]
        public void PlayerStartsOutWithNoGetOutOfJailCards_HasGetOutOfJailCardReturnsFalse()
        {
            Assert.False(player.HasGetOutOfJailCard());
        }

        [Test]
        public void AddingAGetOutOfJailCardCorrectlyAdjustsCardBalance()
        {
            player.AddGetOutOfJailCard(mockCard.Object);

            Assert.True(player.HasGetOutOfJailCard());
        }

        [Test]
        public void UsingAGetOutOfJailCard_DecrementsCardBalance()
        {
            player.AddGetOutOfJailCard(mockCard.Object);

            player.SurrenderGetOutOfJailCard();

            Assert.False(player.HasGetOutOfJailCard());
        }
    }
}
