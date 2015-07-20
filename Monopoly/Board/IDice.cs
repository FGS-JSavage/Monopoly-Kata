namespace Monopoly.Board
{
    public interface IDice
    {
        int Score { get; }
        bool WasDoubles { get; }
        void Roll();
    }
}