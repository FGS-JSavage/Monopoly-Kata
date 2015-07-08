using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace Monopoly
{
    public class PlayerFactory
    {
        public static List<IPlayer> BuildPlayers(int count)
        {
            List<IPlayer> players = new List<IPlayer>();

            for (int i = 0; i < 6; i++)
            {
                players.Add(new Player(new GoLocation()));
            }

            return players.OrderBy(a => Guid.NewGuid()).ToList(); // Could be better, these arn't guaranteed to be random
        }
    }
}
