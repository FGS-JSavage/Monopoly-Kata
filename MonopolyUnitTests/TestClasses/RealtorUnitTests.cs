using System.Collections.Generic;
using Autofac.Extras.Moq;
using Monopoly;
using Monopoly.Ninject;
using Moq;
using Ninject;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace MonopolyUnitTests
{
    internal class RealtorUnitTests
    {
        private ITurnHandler turnHandler;
        private IRealtor realtor;
        private IPlayer player1;
        private IPlayer player2;

        [SetUp]
        public void Init()
        {
            IKernel ninject = new StandardKernel(new BindingsModule());
            
            turnHandler = ninject.Get<ITurnHandler>();
            realtor = ninject.Get<IRealtor>();

            player1 = ninject.Get<IPlayer>();
            player2 = ninject.Get<IPlayer>();

            //player1 = new Player(new GoLocation());
            //player2 = new Player(new GoLocation());
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
    }
}
