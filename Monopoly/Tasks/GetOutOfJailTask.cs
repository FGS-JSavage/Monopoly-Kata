using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Tasks
{
    public class GetOutOfJailTask : IPlayerTask
    {
        private ITaskHandler taskHandler;

        public GetOutOfJailTask(ITaskHandler taskHandler)
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
