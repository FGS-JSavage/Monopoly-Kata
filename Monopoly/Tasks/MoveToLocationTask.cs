using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Tasks
{
    public class MoveToLocationTask : IPlayerTask
    {
        private int destinationSpaceNumber;
        private Board board;

        public MoveToLocationTask(Board board)
        {
            this.board = board;
        }

        public void Complete(IPlayer player)
        {
            board.MovePlayerDirectlyToSpace(player, destinationSpaceNumber);
        }
    }
}
