using System;
using System.Collections.Generic;
using System.Linq;
using Monopoly.Board.Locations;

namespace Monopoly
{
    public class PlayerFactory
    {
        public static List<IPlayer> BuildPlayers(int count)
        {
            List<IPlayer> players = new List<IPlayer>();
            Random random = new Random();

            for (int i = 0; i < count; i++)
            {
                players.Add(new Player(new GoLocation()));
            }

            return players.OrderBy(x => Guid.NewGuid()).ToList();
        }

        public static List<IPlayer> BuildPlayers(List<string> names)
        {
            List<IPlayer> players = new List<IPlayer>();
            Random random = new Random();

            foreach (string name in names)
            {
                IPlayer player = new Player(new GoLocation());
                player.Name = name;
                players.Add(player);
            }

            return players.OrderBy(x => Guid.NewGuid()).ToList();
        }
    }
}
