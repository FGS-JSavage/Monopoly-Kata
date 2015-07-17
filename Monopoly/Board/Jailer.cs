using System.Collections.Generic;

namespace Monopoly.Board
{
    public interface IJailer
    {
        void Imprison(IPlayer player);
        void ReleasePlayerFromJail(IPlayer player);
        bool PlayerIsImprisoned(IPlayer player);
        int GetRemainingSentence(IPlayer player);
        void DecreaseSentence(IPlayer player);
    }

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
