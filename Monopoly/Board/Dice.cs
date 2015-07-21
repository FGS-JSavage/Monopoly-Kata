using System;

namespace Monopoly.Board
{
    public class Dice : IDice
    {
        protected int dieOneScore;
        protected int dieTwoScore;

        public virtual int Score       { get { return dieOneScore + dieTwoScore; } }
        public virtual bool WasDoubles { get { return dieOneScore == dieTwoScore; } }

        public void Roll()
        {
            dieOneScore = Die.RollDie();
            dieTwoScore = Die.RollDie();
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
