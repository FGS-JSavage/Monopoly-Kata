
using Monopoly.Player;

namespace Monopoly.Tasks
{
    public class MoveDistanceTask : IPlayerTask
    {
        private ITaskHandler taskHandler;
        private int distance;

        public MoveDistanceTask(int distance, ITaskHandler taskHandler)
        {
            this.distance = distance;
            this.taskHandler = taskHandler;
        }

        public void Complete(IPlayer player)
        {
            taskHandler.HandleMoveDistance(distance, player);
        }
    }
}
