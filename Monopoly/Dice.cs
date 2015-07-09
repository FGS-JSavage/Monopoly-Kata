using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity.InterceptionExtension;
using Moq.Protected;

namespace Monopoly
{
    public interface IDice
    {
        int Score { get; }
        bool WasDoubles { get; }
        void Roll();
    }

    public class Dice : IDice
    {
        protected int dieOneScore;
        protected int dieTwoScore;
    

        public int Score       { get { return dieOneScore + dieTwoScore; } }
        public bool WasDoubles { get { return dieOneScore == dieTwoScore; } }


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
