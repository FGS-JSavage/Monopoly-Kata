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
        private TurnHandler turnHandler;

        public Game(int numberOfPlayers = DEFAULT_NUMBER_OF_PLAYERS)
        {
            players = PlayerFactory.BuildPlayers(numberOfPlayers);

            for (int i = 0; i < numberOfPlayers; i++)
            {
                players.Add(new Player(new GoLocation()));
            }
        }

        public void DoTurn(IPlayer player)
        {
            turnHandler.DoTurn(player);
        }

        public List<IPlayer> GetPlayers()
        {
            throw new NotImplementedException();
        }

        public void DoRound()
        {
            foreach (var player in players)
            {
                DoTurn(player);
            }
        }

        public List<IPlayer> Player()
        {
            return players;
        }

        public TurnHandler GetBoard()
        {
            return turnHandler;
        }
    }
}
