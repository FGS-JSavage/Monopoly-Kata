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
        
        private int spaceNumber;
        private Board board;

        private ILocation Location { get; set; }
        public int Balance         { get; set; }

        public Player(ILocation location, Board board, int startingBalance = DEFAULT_STARTING_BALANCE)
        {
            this.Location = location;
            this.board    = board;
            this.Balance  = startingBalance;
        }
    }
}
