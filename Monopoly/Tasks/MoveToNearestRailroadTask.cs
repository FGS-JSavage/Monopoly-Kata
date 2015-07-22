
using Monopoly.Player;

namespace Monopoly.Tasks
{
    public class MoveToNearestRailroadTask : IPlayerTask
    {
        private ITaskHandler taskHandler;

        public MoveToNearestRailroadTask(ITaskHandler taskHandler)
        {
            this.taskHandler = taskHandler;
        }

        public void Complete(IPlayer player)
        {
            taskHandler.HandleMoveToNearestRailroad(player);
        }
    }
}
