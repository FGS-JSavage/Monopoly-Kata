using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Tasks
{
    public class GetOutOfJailTask : IPlayerTask
    {
        private Board board;

        public GetOutOfJailTask(Board board)
        {
            this.board = board;
        }
        public void Complete(IPlayer player)
        {
            // TODO 
            //board.ReleasePlayerFromJail(player);
            //player.DecrementGetOutOfJailCard();
        }
    }
}
