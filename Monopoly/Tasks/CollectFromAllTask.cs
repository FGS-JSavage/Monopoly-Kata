using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Tasks
{
    class CollectFromAllTask : IPlayerTask
    {
        private Banker banker;
        private int amount;
        private List<IPlayer> players; 

        public CollectFromAllTask(int amount, List<IPlayer> players, Banker banker)
        {
            this.amount = amount;
            this.players = players;
            this.banker = banker;
        }

        public void Complete(IPlayer player)
        {
            players.ForEach(x => banker.Transfer(x, player, amount));
        }
    }
}
