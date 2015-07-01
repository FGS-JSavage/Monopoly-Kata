using Autofac.Extras.Moq;
using Monopoly;
using Moq;
using NUnit.Framework;

namespace MonopolyUnitTests
{
    internal class RealtorUnitTests
    {
        private Realtor mockRealtor;
        private AutoMock mocker;
        private Mock<Player> mockPlayer1;
        private Mock<Player> mockPlayer2;

        private Realtor realtor;
        private Player player1;
        private Player player2;
        private Mock<Banker> banker;

        [SetUp]
        public void Init()
        {
            mocker = AutoMock.GetLoose();

            banker = new Mock<Banker>();

            mockRealtor = new Realtor(banker.Object);

            mockPlayer1 = new Mock<Player>();
            mockPlayer2 = new Mock<Player>();

            mocker.Provide(banker);
            realtor = new Realtor(banker.Object);
        }

        [Test]
        public void CalculateRentForRailroad_WhenOneIsOwned_RentIs25()
        {
            realtor.SetOwnerForSpace(mockPlayer1, 5);

            Assert.AreEqual(25, realtor.CalculateRent(5, 0));

        }

        [Test]
        public void CalculateRentForRailroad_WhenTwoAreOwnedBySamePlayer_RentIs50()
        {
            realtor.SetOwnerForSpace(mockPlayer1, 5);
            realtor.SetOwnerForSpace(mockPlayer1, 15);

            Assert.AreEqual(50, realtor.CalculateRent(5, 0));
        }

        [Test]
        public void CalculateRentForRailroad_WhenThreeAreOwnedBySamePlayer_RentIs75()
        {
            realtor.SetOwnerForSpace(mockPlayer1, 5);
            realtor.SetOwnerForSpace(mockPlayer1, 15);
            realtor.SetOwnerForSpace(mockPlayer1, 25);

            Assert.AreEqual(75, realtor.CalculateRent(5, 0));
        }

        [Test]
        public void CalculateRentForRailroad_WhenAllAreOwnedBySamePlayer_RentIs100()
        {
            realtor.SetOwnerForSpace(mockPlayer1, 5);
            realtor.SetOwnerForSpace(mockPlayer1, 15);
            realtor.SetOwnerForSpace(mockPlayer1, 25);
            realtor.SetOwnerForSpace(mockPlayer1, 35);

            Assert.AreEqual(100, realtor.CalculateRent(5, 0));
        }

        [Test]
        public void CalculateRentForRailroad_WhenTwoAreOwnedBySamePlayerAndAnotherIsOwnedByADifferentPlayer_RentIs50()
        {
            realtor.SetOwnerForSpace(mockPlayer1, 5);
            realtor.SetOwnerForSpace(mockPlayer1, 15);
            realtor.SetOwnerForSpace(mockPlayer2, 25);

            Assert.AreEqual(50, realtor.CalculateRent(5, 0));
        }

        [Test]
        public void CalculateRentForProperty_WhenOnlyTheLandedOnPropertyIsOwned_RentIsStandard()
        {
            var expectedRent = 10;

            realtor.SetOwnerForSpace(mockPlayer1, 11);

            Assert.AreEqual(expectedRent, realtor.CalculateRent(11, 0));
        }

        [Test]
        public void CalculateRentForProperty_WhenItAndAnotherPropertyOfSameGroupAreOwned_RentIsStandard()
        {
            var expectedRent = 10;

            realtor.SetOwnerForSpace(mockPlayer1, 11);
            realtor.SetOwnerForSpace(mockPlayer1, 13);


            Assert.AreEqual(expectedRent, realtor.CalculateRent(11, 0));
        }

        [Test]
        public void CalculateRentForProperty_WhenAllPropertyOfSameGroupAreOwned_RentIsDouble()
        {
            var expectedRent = 20;

            realtor.SetOwnerForSpace(mockPlayer1, 11);
            realtor.SetOwnerForSpace(mockPlayer1, 13);
            realtor.SetOwnerForSpace(mockPlayer1, 14);


            Assert.AreEqual(expectedRent, realtor.CalculateRent(11, 0));
        }

        [Test]
        public void CalculateRentForUtility_WhenOneIsOwned()
        {
            realtor.SetOwnerForSpace(mockPlayer1, 12);

            Assert.AreEqual(20, realtor.CalculateRent(12, 5));
        }

        [Test]
        public void CalculateRentForUtility_WhenBothAreOwnedBySamePlayer()
        {
            realtor.SetOwnerForSpace(mockPlayer1, 12);
            realtor.SetOwnerForSpace(mockPlayer1, 28);

            Assert.AreEqual(50, realtor.CalculateRent(12, 5));
        }

        [Test]
        public void CalculateRentForUtility_WhenBothAreOwnedByDifferentPlayers()
        {
            realtor.SetOwnerForSpace(mockPlayer1, 12);
            realtor.SetOwnerForSpace(mockPlayer2, 28);

            Assert.AreEqual(50, realtor.CalculateRent(12, 5));
        }

        [Test] // TODO figure out how the hell to do this
        public void ChargeRent_CorrectlyTransfersFunds()
        {
            Mock<Realtor> real = new Mock<Realtor>();
            var moneyToBeTransferred = 20;
            real.Setup(x => x.CalculateRent(It.IsAny<int>(), It.IsAny<int>())).Returns(moneyToBeTransferred);
            


            mockRealtor.ChargeRent(mockPlayer1, mockPlayer2, 5);

            banker.Verify(x => x.Transfer(It.IsAny<Player>(), It.IsAny<Player>(), moneyToBeTransferred));
        }
    }
}
