using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public interface IDeck
    {
        ICard Draw();
        void Discard(ICard card);
    }
    public class Deck : IDeck
    {
        private Queue<ICard> cards; 
        
        public Deck(List<ICard> cards)
        {
            this.cards = new Queue<ICard>(cards);
        }

        public ICard Draw()
        {
            return cards.Dequeue();
        }

        public void Discard(ICard card)
        {
            cards.Enqueue(card);
        }

    }
}
