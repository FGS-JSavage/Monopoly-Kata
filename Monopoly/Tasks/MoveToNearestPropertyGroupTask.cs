using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Tasks
{
    public class MoveToNearestPropertyGroupTask : IPlayerTask
    {
        private ITaskHandler taskHandler;

        public MoveToNearestPropertyGroupTask(PropertyGroup targetGroup, ITaskHandler taskHandler)
        {
            this.taskHandler = taskHandler;
        }


        public void Complete(IPlayer player)
        {
            taskHandler.MoveToClosest(player, PropertyGroup.Utility);
        }
    }
}
