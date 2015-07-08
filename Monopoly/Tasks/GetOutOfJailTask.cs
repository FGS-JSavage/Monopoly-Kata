using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Tasks
{
    public class GetOutOfJailTask : IPlayerTask
    {
        private TurnHandler _turnHandler;

        public GetOutOfJailTask(TurnHandler _turnHandler)
        {
            this._turnHandler = _turnHandler;
        }
        public void Complete(IPlayer player)
        {
            // TODO 
            //TurnHandler.ReleasePlayerFromJail(player);
            //player.DecrementGetOutOfJailCard();
        }
    }
}
