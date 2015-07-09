using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Cards;

namespace Monopoly.Tasks
{
    public class DrawChestTask : IPlayerTask
    {
        private TaskHandler taskHandler;

        public DrawChestTask(TaskHandler taskHandler)
        {
            this.taskHandler = taskHandler;
        }


        public void Complete(IPlayer player)
        {
            taskHandler.HandleDrawChest(player);
        }
    }
}
