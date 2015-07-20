namespace Monopoly.Cards
{
    public interface IDeck
    {
        ICard Draw();
        void Discard(ICard card);
    }
}