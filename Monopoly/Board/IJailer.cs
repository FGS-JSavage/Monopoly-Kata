namespace Monopoly.Board
{
    public interface IJailer
    {
        void Imprison(IPlayer player);
        void ReleasePlayerFromJail(IPlayer player);
        bool PlayerIsImprisoned(IPlayer player);
        int GetRemainingSentence(IPlayer player);
        void DecreaseSentence(IPlayer player);
    }
}