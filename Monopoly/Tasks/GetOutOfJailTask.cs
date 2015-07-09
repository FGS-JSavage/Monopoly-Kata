using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Tasks
{
    public class GetOutOfJailTask : IPlayerTask
    {
        private TaskHandler taskHandler;

        public GetOutOfJailTask(TaskHandler taskHandler)
        {
            this.taskHandler = taskHandler;
        }
        public void Complete(IPlayer player)
        {
            // TODO 
            //TurnHandler.ReleasePlayerFromJail(player);
            //player.DecrementGetOutOfJailCard();
        }
    }
}
