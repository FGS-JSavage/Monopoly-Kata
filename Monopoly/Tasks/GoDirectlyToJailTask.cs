
using Monopoly.Player;

namespace Monopoly.Tasks
{
    public class GoDirectlyToJailTask : IPlayerTask
    {
        private ITaskHandler taskHandler;

        public GoDirectlyToJailTask(ITaskHandler taskHandler)
        {
            this.taskHandler = taskHandler;
        }

        public void Complete(IPlayer player)
        {
            taskHandler.HandleGoDirectlyToJail(player);
        }
    }
}
