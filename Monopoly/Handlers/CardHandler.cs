using Monopoly.Cards;

namespace Monopoly.Handlers
{
    public class CardHandler : ICardHandler
    {
        private IDeck chanceDeck;
        private IDeck chestDeck;

        public CardHandler(IDeckFactory deckFactory)
        {
            chanceDeck = deckFactory.BuildChanceDeck();
            chestDeck  = deckFactory.BuildCommunitiyChestDeck();
        }

        public ICard DrawChanceCard()
        {
            return chanceDeck.Draw();
        }

        public ICard DrawChestCard()
        {
            return chestDeck.Draw();
        }

        public void Discard(ICard card)
        {
            if (card.Type == DeckType.Chance)
            {
                chanceDeck.Discard(card);
            }
            else
            {
                chestDeck.Discard(card);
            }
        }
    }
}
