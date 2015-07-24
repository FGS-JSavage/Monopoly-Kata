
using Monopoly.Player;

namespace Monopoly.Tasks
{
    public class PayIncomeTaxTask : IPlayerTask
    {
        public void Complete(IPlayer player)
        {
            player.Balance -= player.Balance * 0.1 < 200 ? player.Balance * 0.1 : 200;
        }
    }
}
