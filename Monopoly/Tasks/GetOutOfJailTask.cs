
using Monopoly.Player;

namespace Monopoly.Tasks
{
    public class GetOutOfJailTask : IPlayerTask
    {
        private ITaskHandler taskHandler;

        public GetOutOfJailTask(ITaskHandler taskHandler)
        {
            this.taskHandler = taskHandler;
        }
        public void Complete(IPlayer player)
        {
            // Nothing to do here 
        }
    }
}
