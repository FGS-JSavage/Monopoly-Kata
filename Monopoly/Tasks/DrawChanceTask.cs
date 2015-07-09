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
        private Deck deck;

        public DrawChanceTask(Deck deck)
        {
            this.deck = deck;
        }

        public virtual void Complete(IPlayer player)
        {
            var card = deck.Draw();

            if (card.Name == "Get Out of Jail Card")
            {
                player.AddGetOutOfJailCard();
            }
            else
            {
                card.Tasks.ForEach(x => x.Complete(player));
                deck.Discard(card);    
            }
        }
    }
}
