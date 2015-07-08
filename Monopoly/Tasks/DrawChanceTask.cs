using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Tasks
{
    public class DrawChanceTask : IPlayerTask
    {
        private Board board;

        public DrawChanceTask(Board board)
        {
            this.board = board;
        }

        public virtual void Complete(IPlayer player)
        {
            var card = board.DrawChest();

            card.Tasks.ForEach(x => x.Complete(player));
            board.DiscardChest(card);
        }
    }
}
