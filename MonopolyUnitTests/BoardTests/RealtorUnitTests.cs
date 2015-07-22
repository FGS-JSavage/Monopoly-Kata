using System;
using Monopoly.Board;
using Monopoly.Ninject;
using Monopoly.Player;
using Moq;
using Ninject;
using NUnit.Framework;

namespace MonopolyUnitTests.BoardTests
{
    class RealtorUnitTests
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
        public void ChargeDoubleRailroadRent_ChargesTwiceTheDefaultAmount()
        {
            int startingBalance = 100;
            int doubleRent = 50;

            player1.Balance = startingBalance;

            realtor.SetOwnerForSpace(player2, 5);
            player1.PlayerLocation = realtor.LocationForSpaceNumber(5);

            realtor.ChargeDoubleRailroadRent(player1);

            Assert.AreEqual(startingBalance - doubleRent, player1.Balance);
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

        [Test]
        public void ChargeTenTimesRollValueRent_ChargesCorrectAmount()
        {
            var rollValue = 10;
            var expectedRent = 10*rollValue;
            var intitalBalance = player1.Balance;

            realtor.SetOwnerForSpace(player2, 1);
            player1.PlayerLocation = realtor.LocationForSpaceNumber(1);

            realtor.ChargeTenTimesRollValueRent(player1, rollValue);

            Assert.AreEqual(expectedRent, Math.Abs(intitalBalance - player1.Balance));
        }

        [Test]
        public void SetOwnerForSpace_CorrectlySetsOwnerForSpecifiedSpace()
        {
            var someSpace = 1;

            realtor.SetOwnerForSpace(player1, someSpace);

            Assert.AreEqual(realtor.GetOwnerForSpace(someSpace), player1);
        }

        [Test]
        public void ASpaceIsUnowned_SpaceIsOwnedReturnsFalse()
        {
            Assert.False(realtor.SpaceIsOwned(4));
        }

        [Test]
        public void APlayerOwnsASpace_SpaceIsOwnedReturnsTrue()
        {
            var spaceNumber = 4;
            realtor.SetOwnerForSpace(player1, spaceNumber);

            Assert.True(realtor.SpaceIsOwned(spaceNumber));
        }

        [Test]
        public void SpaceIsForSale_ReturnsTrueForUnownedRentableLocations()
        {
            var spaceNumberOfRentableLocation = 1;
            
            Assert.True(realtor.SpaceIsForSale(spaceNumberOfRentableLocation));
        }

        [Test]
        public void SpaceIsForSale_ReturnsFalseForOwnedRentableLocations()
        {
            var spaceNumberOfOwnedRentableLocation = 1;

            realtor.SetOwnerForSpace(player2, spaceNumberOfOwnedRentableLocation);

            Assert.False(realtor.SpaceIsForSale(spaceNumberOfOwnedRentableLocation));
        }

        [Test]
        public void SpaceIsForSale_ReturnsFalseForNonRentableLocations()
        {
            var spaceNumberOfNonRentableLocation = 0;

            Assert.False(realtor.SpaceIsForSale(spaceNumberOfNonRentableLocation));
        }

        [Test]
        [TestCase(0, Result = 5)]
        [TestCase(20, Result = 25)]
        [TestCase(19, Result = 15)]
        [TestCase(39, Result = 35)]
        public int GetClsoest_ReturnsClosestLocationOfDesiredPropertyGroup(int startingSpaceNumber)
        {
            var desiredPropertyGroup = PropertyGroup.Railroad;

            return realtor.GetClosest(startingSpaceNumber, desiredPropertyGroup).SpaceNumber;
        }


    }
}
