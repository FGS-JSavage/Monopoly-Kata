using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Tasks
{
    public class GoDirectlyToJailTask : IPlayerTask
    {
        private TaskHandler taskHandler;

        public GoDirectlyToJailTask(TaskHandler taskHandler)
        {
            this.taskHandler = taskHandler;
        }
        public void Complete(IPlayer player)
        {
            taskHandler.SendPlayerToJail(player);
        }
    }
}
