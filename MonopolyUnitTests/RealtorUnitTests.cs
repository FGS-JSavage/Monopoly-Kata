using Autofac.Extras.Moq;
using Monopoly;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace MonopolyUnitTests
{
    internal class RealtorUnitTests
    {
        private AutoMock mocker;

        private Realtor realtor;

        private Mock<Realtor> mockRealtor;
        private Mock<Player> mockPlayer1;
        private Mock<Player> mockPlayer2;

        private Mock<Banker> mockBanker;

        [SetUp]
        public void Init()
        {
            mocker = AutoMock.GetLoose();


            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            mockPlayer1 = fixture.Create<Mock<Player>>();
            mockPlayer2 = fixture.Create<Mock<Player>>();

            //mockBanker = new Mock<Banker>(); // Old
            mockBanker = fixture.Create<Mock<Banker>>();
            
            //mocker.Provide(mockBanker); // Not sure If I need this
            realtor = new Realtor(mockBanker.Object);

            mockRealtor = fixture.Create<Mock<Realtor>>();


        }

        [Test]
        public void CalculateRentForRailroad_WhenOneIsOwned_RentIs25()
        {
            realtor.SetOwnerForSpace(mockPlayer1.Object, 5);

            Assert.AreEqual(25, realtor.CalculateRent(5, 0));
        }

        [Test]
        public void CalculateRentForRailroad_WhenTwoAreOwnedBySamePlayer_RentIs50()
        {
            realtor.SetOwnerForSpace(mockPlayer1.Object, 5);
            realtor.SetOwnerForSpace(mockPlayer1.Object, 15);

            Assert.AreEqual(50, realtor.CalculateRent(5, 0));
        }

        [Test]
        public void CalculateRentForRailroad_WhenThreeAreOwnedBySamePlayer_RentIs75()
        {
            realtor.SetOwnerForSpace(mockPlayer1.Object, 5);
            realtor.SetOwnerForSpace(mockPlayer1.Object, 15);
            realtor.SetOwnerForSpace(mockPlayer1.Object, 25);

            Assert.AreEqual(75, realtor.CalculateRent(5, 0));
        }

        [Test]
        public void CalculateRentForRailroad_WhenAllAreOwnedBySamePlayer_RentIs100()
        {
            realtor.SetOwnerForSpace(mockPlayer1.Object, 5);
            realtor.SetOwnerForSpace(mockPlayer1.Object, 15);
            realtor.SetOwnerForSpace(mockPlayer1.Object, 25);
            realtor.SetOwnerForSpace(mockPlayer1.Object, 35);

            Assert.AreEqual(100, realtor.CalculateRent(5, 0));
        }

        [Test]
        public void CalculateRentForRailroad_WhenTwoAreOwnedBySamePlayerAndAnotherIsOwnedByADifferentPlayer_RentIs50()
        {
            realtor.SetOwnerForSpace(mockPlayer1.Object, 5);
            realtor.SetOwnerForSpace(mockPlayer1.Object, 15);
            realtor.SetOwnerForSpace(mockPlayer2.Object, 25);

            Assert.AreEqual(50, realtor.CalculateRent(5, 0));
        }

        [Test]
        public void CalculateRentForProperty_WhenOnlyTheLandedOnPropertyIsOwned_RentIsStandard()
        {
            var expectedRent = 10;

            realtor.SetOwnerForSpace(mockPlayer1.Object, 11);

            Assert.AreEqual(expectedRent, realtor.CalculateRent(11, 0));
        }

        [Test]
        public void CalculateRentForProperty_WhenItAndAnotherPropertyOfSameGroupAreOwned_RentIsStandard()
        {
            var expectedRent = 10;

            realtor.SetOwnerForSpace(mockPlayer1.Object, 11);
            realtor.SetOwnerForSpace(mockPlayer1.Object, 13);


            Assert.AreEqual(expectedRent, realtor.CalculateRent(11, 0));
        }

        [Test]
        public void CalculateRentForProperty_WhenAllPropertyOfSameGroupAreOwned_RentIsDouble()
        {
            var expectedRent = 20;

            realtor.SetOwnerForSpace(mockPlayer1.Object, 11);
            realtor.SetOwnerForSpace(mockPlayer1.Object, 13);
            realtor.SetOwnerForSpace(mockPlayer1.Object, 14);

            Assert.AreEqual(expectedRent, realtor.CalculateRent(11, 0));
        }

        [Test]
        public void CalculateRentForUtility_WhenOneIsOwned()
        {
            realtor.SetOwnerForSpace(mockPlayer1.Object, 12);

            Assert.AreEqual(20, realtor.CalculateRent(12, 5));
        }

        [Test]
        public void CalculateRentForUtility_WhenBothAreOwnedBySamePlayer()
        {
            realtor.SetOwnerForSpace(mockPlayer1.Object, 12);
            realtor.SetOwnerForSpace(mockPlayer1.Object, 28);

            Assert.AreEqual(50, realtor.CalculateRent(12, 5));
        }

        [Test]
        public void CalculateRentForUtility_WhenBothAreOwnedByDifferentPlayers()
        {
            mockRealtor.Object.SetOwnerForSpace(mockPlayer1.Object, 12);
            mockRealtor.Object.SetOwnerForSpace(mockPlayer2.Object, 28);

            Assert.AreEqual(50, realtor.CalculateRent(12, 5));
        }

        [Test] // TODO figure out how the hell to do this
        public void ChargeRent_CorrectlyTransfersFunds()
        {
            //Mock<Realtor> real = new Mock<Realtor>();
            var moneyToBeTransferred = 20;
            mockRealtor.Setup(x => x.CalculateRent(It.IsAny<int>(), It.IsAny<int>())).Returns(moneyToBeTransferred);


            mockRealtor.Object.ChargeRent(mockPlayer1.Object, mockPlayer2.Object, 5);

            mockBanker.Verify(x => x.Transfer(It.IsAny<Player>(), It.IsAny<Player>(), moneyToBeTransferred));
        }
    }
}
