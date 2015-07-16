using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Tasks
{
    public class MoveToNearestRailroadTask : IPlayerTask
    {
        private ITaskHandler taskHandler;

        public MoveToNearestRailroadTask(ITaskHandler taskHandler)
        {
            this.taskHandler = taskHandler;
        }

        public void Complete(IPlayer player)
        {
            taskHandler.HandleMoveToNearestRailroad(player);
        }
    }
}
