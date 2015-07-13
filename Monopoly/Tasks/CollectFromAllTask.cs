using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Tasks
{
    class CollectFromAllTask : IPlayerTask
    {
        private ITaskHandler taskHandler;
        private int amount;

        public CollectFromAllTask(int amount, ITaskHandler taskHandler)
        {
            this.taskHandler = taskHandler;
            this.amount = amount;
        }

        public void Complete(IPlayer player)
        {
            taskHandler.HandleCollectFromAllPlayersTask(player, amount);
        }
    }
}
