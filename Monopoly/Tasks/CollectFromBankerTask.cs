using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Handlers;

namespace Monopoly.Tasks
{
    public class CollectFromBankerTask : IPlayerTask
    {
        private int amount;
        private TaskHandler taskHandler;

        public CollectFromBankerTask(TaskHandler taskHandler, int amount)
        {
            this.taskHandler = taskHandler;
            this.amount = amount;
        }

        public void Complete(IPlayer player)
        {
            player.Balance += amount;
        }
    }
}
