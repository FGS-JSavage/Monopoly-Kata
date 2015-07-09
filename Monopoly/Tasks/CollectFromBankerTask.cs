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
        private TaskHandler taskHandler;
        private int amount;
        
        public CollectFromBankerTask(int amount, TaskHandler taskHandler)
        {
            this.taskHandler = taskHandler;
            this.amount = amount;
        }

        public void Complete(IPlayer player)
        {
            taskHandler.HandleCollectFromBankerTask(player, amount);
        }
    }
}
