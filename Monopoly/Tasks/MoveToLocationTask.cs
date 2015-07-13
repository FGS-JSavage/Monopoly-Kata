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
        private ITaskHandler taskHandler;

        public MoveToLocationTask(int destinationSpaceNumber, ITaskHandler taskHandler)
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
