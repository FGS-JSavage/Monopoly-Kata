using System.Linq;
using Monopoly.Board;
using NUnit.Framework;

namespace MonopolyUnitTests.BoardTests
{
    [TestFixture]
    class DiceUnitTests
    {
        private Dice dice;
        
        [SetUp]
        public void Init()
        {
            dice = new Dice();
        }

        [Test]
        public void DiceRollValueShouldAlwaysBeBetweenTwoAndTwelve()
        {
            foreach (var i in Enumerable.Range(0, 100))
            {
                dice.Roll();
                Assert.LessOrEqual(dice.Score, 12);
                Assert.GreaterOrEqual(dice.Score, 2);
            }            
        }
    }
}
