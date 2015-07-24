using Monopoly.Player;

namespace Monopoly.Board
{
    public interface IBanker
    {
        void ChargePlayerToGetOutOfJail(IPlayer player);
        void Collect(IPlayer player, int amount);
        void Payout(IPlayer player, int amount);
        void Transfer(IPlayer payer, IPlayer recipient, int amount);
    }
}