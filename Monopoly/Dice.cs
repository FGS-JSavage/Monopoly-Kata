using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class Dice
    {
        private const int DEFAULT_DICE = 2;
        private const int DEFAULT_DICE_SIDES = 6;

        private readonly int numberOfDiceToRoll;
        private readonly int numberOfDiceSides;

        public Dice(int diceCount = DEFAULT_DICE, int sideCount = DEFAULT_DICE_SIDES)
        {
            numberOfDiceToRoll = diceCount;
            numberOfDiceSides = sideCount;
        }

        public int Roll()
        {
            return Enumerable.Sum(Enumerable.Repeat(0, numberOfDiceToRoll).Select(i => Die.RollDie(numberOfDiceSides)));
        }

        private static class Die
        {
            private const int DEFAULT_SIDES = 6;
            private static Random random = new Random();

            public static int RollDie(int sides = DEFAULT_SIDES)
            {
                return random.Next(sides) + 1;
            }
        }
    }
}
