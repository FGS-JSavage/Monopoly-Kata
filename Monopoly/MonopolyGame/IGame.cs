using System.Collections.Generic;
using Monopoly.Player;

namespace Monopoly.MonopolyGame
{
    public interface IGame
    {
        void DoRound();
        void DoTurn(IPlayer player);
        List<IPlayer> GetPlayers();
    }
}
