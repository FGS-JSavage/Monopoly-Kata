using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Cards;

namespace Monopoly.Tasks
{
    public class DrawChestTask : IPlayerTask
    {
        private Board board;

        public DrawChestTask(Board board)
        {
            this.board = board;
        }


        public void Complete(IPlayer player)
        {
            var card = board.DrawChest();

            if (card.GetType() == typeof (GetOutOfJailCard))
            {
                player.AddGetOutOfJailCard();
            }
            else
            {
                card.Tasks.ForEach(x => x.Complete(player));
                board.DiscardChest(card);
            }

        }
    }
}
