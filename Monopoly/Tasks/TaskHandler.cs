using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Locations;

namespace Monopoly.Tasks
{
    public class TaskHandler
    {
        private IMovementHandler movementHandler;
        private List<IPlayer> players;
        private IBanker banker;
        private IJailer jailer;

        public TaskHandler(IMovementHandler movementHandler, List<IPlayer> players, IBanker banker, IJailer jailer)
        {
            this.movementHandler = movementHandler;
            this.players = players;
            this.banker = banker;
            this.jailer = jailer;
        }
    }
}
