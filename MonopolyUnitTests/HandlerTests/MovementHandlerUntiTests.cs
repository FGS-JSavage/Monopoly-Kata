using System;
using Monopoly.Handlers;
using Monopoly.Ninject;
using Ninject;
using NUnit.Framework;

namespace MonopolyUnitTests.HandlerTests
{
    [TestFixture]
    class MovementHandlerUntiTests : IDisposable
    {
        private IKernel ninject;

        private MovementHandler movementHandler;

        [SetUp]
        public void Init()
        {
            ninject = new StandardKernel(new BindingsModule());

            movementHandler = ninject.Get<MovementHandler>();
        }

        [TearDown]
        public void Dispose()
        {
            ninject.Dispose();
        }

        [Test]
        [TestCase(-12, Result = 28)]
        [TestCase(-5, Result = 35)]
        [TestCase(0, Result = 0)]
        [TestCase(5, Result = 5)]
        [TestCase(40, Result = 0)]
        [TestCase(60, Result = 20)]
        public int ChompToBoardSize_MapsSpaceNumbersWithinBounds(int spaceNumber)
        {
            return movementHandler.ChompToBoardSize(spaceNumber);
        }
    }
}
