using Monopoly.Player;

namespace Monopoly
{
    public interface ITurnHandler
    {
        void DoTurn(IPlayer player);
    }
}