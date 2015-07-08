using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Tasks
{
    public class MoveToNearestPropertyGroupTask : IPlayerTask
    {
        private TurnHandler turnHandler;

        public MoveToNearestPropertyGroupTask(TurnHandler turnHandler, PropertyGroup targetGroup)
        {
            this.turnHandler = turnHandler;
        }


        public void Complete(IPlayer player)
        {
            turnHandler.MoveToClosest(player, PropertyGroup.Utility);
        }
    }
}
