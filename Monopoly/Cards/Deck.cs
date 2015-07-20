using System.Collections.Generic;

namespace Monopoly.Cards
{
    public class Deck : IDeck
    {
        private Queue<ICard> cards; 
        
        public Deck(List<ICard> cards)
        {
            this.cards = new Queue<ICard>(cards);
        }

        public virtual ICard Draw()
        {
            return cards.Dequeue();
        }

        public void Discard(ICard card)
        {
            cards.Enqueue(card);
        }
    }
}
