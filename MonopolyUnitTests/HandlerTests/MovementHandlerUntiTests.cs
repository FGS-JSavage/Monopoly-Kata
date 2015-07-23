using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Handlers;
using Monopoly.Ninject;
using Ninject;
using NUnit.Framework;

namespace MonopolyUnitTests.HandlerTests
{
    [TestFixture]
    class MovementHandlerUntiTests
    {
        private IKernel ninject;

        private MovementHandler movementHandler;

        [SetUp]
        public void Init()
        {
            IKernel ninject = new StandardKernel(new BindingsModule());

            movementHandler = ninject.Get<MovementHandler>();
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
