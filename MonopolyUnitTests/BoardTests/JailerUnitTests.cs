using Moq;
using Monopoly.Board;
using Monopoly.Player;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace MonopolyUnitTests.BoardTests
{
    [TestFixture]
    class JailerUnitTests
    {
        private Mock<Player> mockPlayer;
        private Jailer jailer;

        [SetUp]
        public void Init()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            mockPlayer = fixture.Create<Mock<Player>>();
            jailer = new Jailer();
        }

        [Test]
        public void PlayerIsNotInPrison_PlayerIsImprisonedReturnsFalse()
        {
            Assert.False(jailer.PlayerIsImprisoned(mockPlayer.Object));
        }


        [Test]
        public void ThePlayerIsImprisoned_PlayerIsImprisonedReturnsTrue()
        {
            jailer.Imprison(mockPlayer.Object);
           
            Assert.True(jailer.PlayerIsImprisoned(mockPlayer.Object));
        }

        [Test]
        public void PlayerIsImprisoned_PlayerStartsWithA3RoundSentence()
        {
            int startingSentence = 3;

            jailer.Imprison(mockPlayer.Object);

            Assert.AreEqual(startingSentence, jailer.GetRemainingSentence(mockPlayer.Object));
        }

        [Test]
        public void PlayerIsImprisoned_CallDecreaseSentence_SentenceIsDecrementedByOne()
        {
            int startingSentence = 3;

            jailer.Imprison(mockPlayer.Object);

            jailer.DecreaseSentence(mockPlayer.Object);

            Assert.AreEqual(startingSentence - 1, jailer.GetRemainingSentence(mockPlayer.Object));
        }

        [Test]
        public void ThePlayerIsImprisoned_PlayerIsReleasedFromJail_PlayerIsImprisonedReturnsFalse()
        {
            jailer.Imprison(mockPlayer.Object);

            jailer.ReleasePlayerFromJail(mockPlayer.Object);

            Assert.False(jailer.PlayerIsImprisoned(mockPlayer.Object));
        }

    }
}