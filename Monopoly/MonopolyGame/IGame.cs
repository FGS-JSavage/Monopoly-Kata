using System.Collections.Generic;

namespace Monopoly.MonopolyGame
{
    public interface IGame
    {
        void DoRound();
        void DoTurn(IPlayer player);
        List<IPlayer> GetPlayers();
    }
}
