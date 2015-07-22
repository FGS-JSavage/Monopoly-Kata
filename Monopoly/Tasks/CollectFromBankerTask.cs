
using Monopoly.Player;

namespace Monopoly.Tasks
{
    public class CollectFromBankerTask : IPlayerTask
    {
        private ITaskHandler taskHandler;
        private int amount;
        
        public CollectFromBankerTask(int amount, ITaskHandler taskHandler)
        {
            this.taskHandler = taskHandler;
            this.amount = amount;
        }

        public void Complete(IPlayer player)
        {
            taskHandler.HandleCollectFromBankerTask(player, amount);
        }
    }
}
