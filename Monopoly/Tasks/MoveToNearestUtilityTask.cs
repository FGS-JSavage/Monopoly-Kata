
using Monopoly.Player;

namespace Monopoly.Tasks
{
    public class MoveToNearestUtilityTask : IPlayerTask
    {
        private ITaskHandler taskHandler;

        public MoveToNearestUtilityTask(ITaskHandler taskHandler)
        {
            this.taskHandler = taskHandler;
        }
        public void Complete(IPlayer player)
        {
            taskHandler.HandleMoveToNearestUtility(player);
        }
    }
}
