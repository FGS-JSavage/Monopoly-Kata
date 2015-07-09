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
        private TurnHandler turnHandler;

        public DrawChanceTask(TurnHandler turnHandler)
        {
            this.turnHandler = turnHandler;
        }

        public virtual void Complete(IPlayer player)
        {
            var card = turnHandler.DrawChest();

            card.Tasks.ForEach(x => x.Complete(player));
            turnHandler.DiscardChest(card);
        }
    }
}
