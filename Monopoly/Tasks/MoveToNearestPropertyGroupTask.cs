using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Tasks
{
    public class MoveToNearestPropertyGroupTask : IPlayerTask
    {
        private Board board;

        public MoveToNearestPropertyGroupTask(Board board, PropertyGroup targetGroup)
        {
            this.board = board;
        }


        public void Complete(IPlayer player)
        {
            board.MoveToClosest(player, PropertyGroup.Utility);
        }
    }
}
