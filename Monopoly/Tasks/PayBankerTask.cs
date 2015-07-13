using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Tasks
{
    public class PayBankerTask : IPlayerTask
    {
        private int amount;
        private ITaskHandler taskHandler;
        
        public PayBankerTask(int amount, ITaskHandler taskHandler)
        {
            this.taskHandler = taskHandler;
            this.amount = amount;
        }

        public void Complete(IPlayer player)
        {
            taskHandler.HandlePayBankerTask(amount, player);
        }
    }
}
