using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Monopoly;

namespace MonopolyUnitTests
{
    
    [TestFixture]
    class DiceUnitTests
    {
        Dice dice;

        [Test]
        public void Roll_Two_Dice_Six_Sided_Score_Is_GTE_2_LTE_12()
        {
            dice = new Dice();

            foreach (var rollValue in Enumerable.Repeat(0, 100).Select(i => dice.Roll()))
            {
                Assert.GreaterOrEqual(rollValue, 2);
                Assert.LessOrEqual(rollValue, 12);
            }
        }
    }
}
