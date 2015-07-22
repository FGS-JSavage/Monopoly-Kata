using Monopoly.Player;

namespace Monopoly.Board
{
    public class Banker : IBanker
    {
        private const int JAIL_RELEASE_FEE = 50;

        public void ChargePlayerToGetOutOfJail(IPlayer player)
        {
            player.Balance -= JAIL_RELEASE_FEE;
        }

        public void Collect(IPlayer player, int amount)
        {
            player.Balance -= amount;
        }

        public void Payout(IPlayer player, int amount)
        {
            player.Balance += amount;
        }

        public virtual void Transfer(IPlayer payer, IPlayer recipient, int amount)
        {
            Collect(payer, amount);
            Payout(recipient, amount);
        }
    }
}
