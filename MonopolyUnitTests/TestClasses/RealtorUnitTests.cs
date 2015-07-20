using Monopoly;
using Monopoly.Board;
using Monopoly.Ninject;
using Moq;
using Ninject;
using NUnit.Framework;

namespace MonopolyUnitTests
{
    internal class RealtorUnitTests
    {
        private IRealtor realtor;
        private IPlayer  player1;
        private IPlayer  player2;

        [SetUp]
        public void Init()
        {
            IKernel ninject = new StandardKernel(new BindingsModule());

            realtor = ninject.Get<IRealtor>();

            player1 = ninject.Get<IPlayer>();
            player2 = ninject.Get<IPlayer>();
        }

        [Test]
        public void CalculateRentForRailroad_WhenOneIsOwned_RentIs25()
        {
            realtor.SetOwnerForSpace(player1, 5);

            Assert.AreEqual(25, realtor.CalculateRent(5, 0));
        }

        [Test]
        public void CalculateRentForRailroad_WhenTwoAreOwnedBySamePlayer_RentIs50()
        {
            realtor.SetOwnerForSpace(player1, 5);
            realtor.SetOwnerForSpace(player1, 15);

            Assert.AreEqual(50, realtor.CalculateRent(5, 0));
        }

        [Test]
        public void CalculateRentForRailroad_WhenThreeAreOwnedBySamePlayer_RentIs75()
        {
            realtor.SetOwnerForSpace(player1, 5);
            realtor.SetOwnerForSpace(player1, 15);
            realtor.SetOwnerForSpace(player1, 25);

            Assert.AreEqual(75, realtor.CalculateRent(5, 0));
        }

        [Test]
        public void CalculateRentForRailroad_WhenAllAreOwnedBySamePlayer_RentIs100()
        {
            realtor.SetOwnerForSpace(player1, 5);
            realtor.SetOwnerForSpace(player1, 15);
            realtor.SetOwnerForSpace(player1, 25);
            realtor.SetOwnerForSpace(player1, 35);

            Assert.AreEqual(100, realtor.CalculateRent(5, 0));
        }

        [Test]
        public void CalculateRentForRailroad_WhenTwoAreOwnedBySamePlayerAndAnotherIsOwnedByADifferentPlayer_RentIs50()
        {
            realtor.SetOwnerForSpace(player1, 5);
            realtor.SetOwnerForSpace(player1, 15);
            realtor.SetOwnerForSpace(player2, 25);

            Assert.AreEqual(50, realtor.CalculateRent(5, 0));
        }

        [Test]
        public void CalculateRentForProperty_WhenOnlyTheLandedOnPropertyIsOwned_RentIsStandard()
        {
            var expectedRent = 10;

            realtor.SetOwnerForSpace(player1, 11);

            Assert.AreEqual(expectedRent, realtor.CalculateRent(11, 0));
        }

        [Test]
        public void CalculateRentForProperty_WhenItAndAnotherPropertyOfSameGroupAreOwned_RentIsStandard()
        {
            var expectedRent = 10;

            realtor.SetOwnerForSpace(player1, 11);
            realtor.SetOwnerForSpace(player1, 13);

            Assert.AreEqual(expectedRent, realtor.CalculateRent(11, 0));
        }

        [Test]
        public void CalculateRentForProperty_WhenAllPropertyOfSameGroupAreOwned_RentIsDouble()
        {
            var expectedRent = 20;

            realtor.SetOwnerForSpace(player1, 11);
            realtor.SetOwnerForSpace(player1, 13);
            realtor.SetOwnerForSpace(player1, 14);

            Assert.AreEqual(expectedRent, realtor.CalculateRent(11, 0));
        }

        [Test]
        public void CalculateRentForUtility_WhenOneIsOwned()
        {
            realtor.SetOwnerForSpace(player1, 12);

            Assert.AreEqual(20, realtor.CalculateRent(12, 5));
        }

        [Test]
        public void CalculateRentForUtility_WhenBothAreOwnedBySamePlayer()
        {
            realtor.SetOwnerForSpace(player1, 12);
            realtor.SetOwnerForSpace(player1, 28);

            Assert.AreEqual(50, realtor.CalculateRent(12, 5));
        }

        [Test]
        public void CalculateRentForUtility_WhenBothAreOwnedByDifferentPlayers()
        {
            realtor.SetOwnerForSpace(player1, 12);
            realtor.SetOwnerForSpace(player2, 28);

            Assert.AreEqual(50, realtor.CalculateRent(12, 5));
        }

        [Test]
        public void ChargeRentCorrectlyTransfersFundsBetweenRenterAndOwner()
        {
            var player1nitialBalance = player1.Balance;
            var player2InitialBalance = player2.Balance;

            var expectedRent = 2;
            
            realtor.SetOwnerForSpace(player1, 1);
            player2.PlayerLocation = realtor.LocationForSpaceNumber(1);


            realtor.ChargeRent(player2, It.IsAny<int>());

            Assert.AreEqual(player1nitialBalance + expectedRent, player1.Balance);
            Assert.AreEqual(player2InitialBalance - expectedRent, player2.Balance);
        }
    }
}
