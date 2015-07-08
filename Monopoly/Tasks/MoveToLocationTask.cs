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
        private MovementHandler movementHandler;

        public MoveToLocationTask(MovementHandler movementHandler, int destinationSpaceNumber)
        {
            this.movementHandler = movementHandler;
            this.destinationSpaceNumber = destinationSpaceNumber;
        }

        public void Complete(IPlayer player)
        {
            movementHandler.MovePlayerDirectlyToSpaceNumber(player, destinationSpaceNumber);
        }
    }
}
