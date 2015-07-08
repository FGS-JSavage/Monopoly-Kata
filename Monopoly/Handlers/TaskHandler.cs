using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Locations;

namespace Monopoly.Handlers
{
    public class TaskHandler
    {
        private MovementHandler movementHandler;
        private List<IPlayer> players;
        private Banker banker;
        private Jailer jailer;

        public TaskHandler(MovementHandler movementHandler, List<IPlayer> players, Banker banker, Jailer jailer)
        {
            this.movementHandler = movementHandler;
            this.players = players;
            this.banker = banker;
            this.jailer = jailer;
        }



        
    }
}
