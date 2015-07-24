
using Monopoly.Player;

namespace Monopoly.Tasks
{
    public class CollectFromAllTask : IPlayerTask
    {
        private ITaskHandler taskHandler;
        private int amount;

        public CollectFromAllTask(int amount, ITaskHandler taskHandler)
        {
            this.taskHandler = taskHandler;
            this.amount = amount;
        }

        public void Complete(IPlayer player)
        {
            taskHandler.HandleCollectFromAllPlayersTask(player, amount);
        }
    }
}
