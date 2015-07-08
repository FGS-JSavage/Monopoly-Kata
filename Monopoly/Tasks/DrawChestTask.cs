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
        private TurnHandler _turnHandler;

        public DrawChestTask(TurnHandler _turnHandler)
        {
            this._turnHandler = _turnHandler;
        }


        public void Complete(IPlayer player)
        {
            var card = _turnHandler.DrawChest();

            if (card.GetType() == typeof (GetOutOfJailCard))
            {
                player.AddGetOutOfJailCard();
            }
            else
            {
                card.Tasks.ForEach(x => x.Complete(player));
                _turnHandler.DiscardChest(card);
            }

        }
    }
}
