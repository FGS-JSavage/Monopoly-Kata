using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Tasks
{
    public class MoveToNearestPropertyGroupTask : IPlayerTask
    {
        private PropertyGroup targetGroup;
        private ITaskHandler taskHandler;
        private int rentMultiplier;

        public MoveToNearestPropertyGroupTask(PropertyGroup targetGroup, int rentMultiplier, ITaskHandler taskHandler)
        {
            this.targetGroup = targetGroup;
            this.rentMultiplier = rentMultiplier;
            this.taskHandler = taskHandler;
        }

        public void Complete(IPlayer player)
        {
            taskHandler.MoveToClosest(player, targetGroup);
        }
    }
}
