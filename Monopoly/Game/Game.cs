using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class Game : IGame
    {
        private const int DEFAULT_NUMBER_OF_PLAYERS = 6;
        private List<IPlayer> players;
        private ITurnHandler turnHandler;

        public Game(ITurnHandler turnHandler, List<IPlayer> players)
        {
            this.players = players.OrderBy(elem => Guid.NewGuid()).ToList(); ;
            this.turnHandler = turnHandler;
        }

        public void DoTurn(IPlayer player)
        {
            turnHandler.DoTurn(player);
        }

        public List<IPlayer> GetPlayers()
        {
            return players;
        }

        public void DoRound()
        {
            foreach (var player in players)
            {
                DoTurn(player);
                player.RoundsPlayed++;
            }
        }

        public List<IPlayer> Player()
        {
            return players;
        }

        public ITurnHandler GetBoard()
        {
            return turnHandler;
        }
    }
}
