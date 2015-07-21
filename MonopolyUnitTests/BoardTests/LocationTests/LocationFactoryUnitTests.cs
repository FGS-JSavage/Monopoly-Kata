using System;
using System.Collections.Generic;
using System.Linq;
using Monopoly.Board.Locations;
using NUnit.Framework;

namespace MonopolyUnitTests.BoardTests.LocationTests
{
    [TestFixture]
    class LocationFactoryUnitTests
    {
        private LocationFactory locationFactory;
        private Dictionary<int, ILocation> locations;

        [SetUp]
        public void Init()
        {
            locationFactory = new LocationFactory();
            locations = locationFactory.BuildLocations();
        }

        [Test]
        public void LocationFactoryBuildsCorrectNumberOfLocations()
        {
            var expectedNumberOfLocations = 40;

            Assert.AreEqual(expectedNumberOfLocations, locations.Count);
        }

        [Test]
        public void AllLocationsHaveKeyValuesBetweenZeroAndForty()
        {
            var upperBound = 40;
            var lowerBound = 0;

            Assert.True(locations.Keys.OrderBy(x => x).First() <= upperBound);
            Assert.True(locations.Keys.OrderBy(x => upperBound - x).First() >= lowerBound);
        }
    }
}
