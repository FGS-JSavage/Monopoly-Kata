
namespace Monopoly.Tasks
{
    public class LandOnGoTask : IPlayerTask
    {
        public void Complete(IPlayer player)
        {
            player.Balance += 200;
        }
    }
}
