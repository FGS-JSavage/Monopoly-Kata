namespace Monopoly.Cards
{
    public interface IDeckFactory
    {
        IDeck BuildCommunitiyChestDeck();
        IDeck BuildChanceDeck();
    }
}