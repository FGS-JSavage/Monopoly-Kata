using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Locations;

namespace Monopoly.Tasks
{
    public class MoveToLocationTask : IPlayerTask
    {
        private int destinationSpaceNumber;
        private TaskHandler taskHandler;

        public MoveToLocationTask(int destinationSpaceNumber, TaskHandler taskHandler)
        {
            this.taskHandler = taskHandler;
            this.destinationSpaceNumber = destinationSpaceNumber;
        }

        public void Complete(IPlayer player)
        {
            taskHandler.HandleMoveToLocationTask(player, destinationSpaceNumber);
        }
    }
}
