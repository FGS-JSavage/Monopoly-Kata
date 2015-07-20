using Monopoly.Cards;

namespace Monopoly.Handlers
{
    public interface ICardHandler
    {
        ICard DrawChanceCard();
        ICard DrawChestCard();
        void Discard(ICard card);
    }
}