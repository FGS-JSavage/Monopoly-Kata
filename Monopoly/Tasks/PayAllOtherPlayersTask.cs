using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Tasks;

namespace Monopoly.Tasks
{
    public class PayAllOtherPlayersTask : IPlayerTask
    {
        private ITaskHandler taskHandler;
        private int amount;

        public PayAllOtherPlayersTask(int amount, ITaskHandler taskHandler)
        {
            this.amount = amount;
            this.taskHandler = taskHandler;
        }

        public void Complete(IPlayer player)
        {
            taskHandler.HandlePayAllOtherPlayers(player, amount);
        }
    }
}
