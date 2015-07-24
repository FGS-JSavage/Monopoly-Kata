
using Monopoly.Player;

namespace Monopoly.Tasks
{
    public class LandOnGoTask : IPlayerTask
    {
        private const int LAND_ON_GO_REWARD = 200;

        public void Complete(IPlayer player)
        {
            player.Balance += LAND_ON_GO_REWARD;
        }
    }
}
