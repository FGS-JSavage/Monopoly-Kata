using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Tasks
{
    public class PayAllOtherPlayersTask : IPlayerTask
    {
        private List<IPlayer> allPlayers;
        private int amount;

        public PayAllOtherPlayersTask(int amount, List<IPlayer> allPlayers)
        {
            this.amount = amount;
            this.allPlayers = allPlayers;
        }

        public void Complete(IPlayer player)
        {
            allPlayers.ForEach(x => {
                x.Balance += amount;
                player.Balance -= amount;
            });
        }
    }
}
