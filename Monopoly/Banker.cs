using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public interface IBanker
    {
        void ChargePlayerToGetOutOfJail(IPlayer player);
        void Collect(IPlayer player, int amount);
        void Payout(IPlayer player, int amount);
        void Transfer(IPlayer payer, IPlayer recipient, int amount);
    }

    public class Banker : IBanker
    {
        public void ChargePlayerToGetOutOfJail(IPlayer player)
        {
            player.Balance -= 50;
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
