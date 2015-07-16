using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Tasks
{
    public class MoveToNearestUtilityTask : IPlayerTask
    {
        private ITaskHandler taskHandler;

        public MoveToNearestUtilityTask(ITaskHandler taskHandler)
        {
            this.taskHandler = taskHandler;
        }
        public void Complete(IPlayer player)
        {
            taskHandler.HandleMoveToNearestUtility(player);
        }
    }
}
