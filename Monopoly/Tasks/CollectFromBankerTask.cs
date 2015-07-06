using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Tasks
{
    public class CollectFromBankerTask : IPlayerTask
    {
        private int amount;

        public CollectFromBankerTask(int amount)
        {
            this.amount = amount;
        }

        public void Complete(IPlayer player)
        {
            player.Balance += amount;
        }
    }
}
