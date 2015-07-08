using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Tasks
{
    public class GoDirectlyToJailTask : IPlayerTask
    {
        private TurnHandler _turnHandler;

        public GoDirectlyToJailTask(TurnHandler _turnHandler)
        {
            this._turnHandler = _turnHandler;
        }
        public void Complete(IPlayer player)
        {
            _turnHandler.SendPlayerToJail(player);
        }
    }
}
