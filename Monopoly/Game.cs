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
        private Dice dice;
        private Board board;

        public Game(int numberOfPlayers = DEFAULT_NUMBER_OF_PLAYERS)
        {
            players = new List<IPlayer>();
            board = new Board();

            for (int i = 0; i < numberOfPlayers; i++)
            {
                players.Add(new Player(new GoLocation()));
            }

            dice = new Dice();
        }

        public void DoTurn(IPlayer player)
        {
            board.DoTurn(player);
        }

        public void DoRound()
        {
            foreach (var player in players)
            {
                DoTurn(player);
            }
        }

        public List<IPlayer> GetPlayers()
        {
            return players;
        }

        public Board GetBoard()
        {
            return board;
        }
    }
}
