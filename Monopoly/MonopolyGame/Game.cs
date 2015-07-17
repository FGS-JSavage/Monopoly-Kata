using System;
using System.Collections.Generic;
using System.Linq;

namespace Monopoly.MonopolyGame
{
    public class Game : IGame
    {
        private List<IPlayer> players;
        private ITurnHandler turnHandler;
        private Random random = new Random();

        public Game(ITurnHandler turnHandler, List<IPlayer> players)
        {
            this.players = players.OrderBy(x => random.Next()).ToList(); ;
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
    }
}
