using System.Collections.Generic;
using Monopoly.Player;

namespace Monopoly.Board
{
    public class Jailer : IJailer
    {
        public Dictionary<IPlayer, int> prisoners;

        public Jailer()
        {
            prisoners = new Dictionary<IPlayer, int>();
        }

        public void Imprison(IPlayer player)
        {
            prisoners.Add(player, 3);
        }

        public int GetRemainingSentence(IPlayer player)
        {
            return prisoners[player];
        }

        public bool PlayerIsImprisoned(IPlayer player)
        {
            return prisoners.ContainsKey(player);
        }

        public void ReleasePlayerFromJail(IPlayer player)
        {
            prisoners.Remove(player);
        }

        public void DecreaseSentence(IPlayer player)
        {
            prisoners[player]--;
        }
    }
}
