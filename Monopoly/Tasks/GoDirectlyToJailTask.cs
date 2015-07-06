using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Tasks
{
    public class GoDirectlyToJailTask : IPlayerTask
    {
        private Board board;

        public GoDirectlyToJailTask(Board board)
        {
            this.board = board;
        }
        public void Complete(IPlayer player)
        {
            board.SendPlayerToJail(player);
        }
    }
}
