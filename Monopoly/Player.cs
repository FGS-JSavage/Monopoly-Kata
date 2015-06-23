using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class Player : IPlayer
    {
        private const int DEFAULT_STARTING_BALANCE = 0;
       
        public ILocation PlayerLocation { get; set; }
        public int Balance              { get; set; }

        public Player(ILocation playerLocation, Board board, int startingBalance = DEFAULT_STARTING_BALANCE)
        {
            this.PlayerLocation = playerLocation;
            this.Balance  = startingBalance;
        }
    }
}
