using Monopoly.Board;
using Monopoly.Player;

namespace Monopoly.Handlers
{
    public interface IMovementHandler
    {
        void MovePlayer(IPlayer player, int distance);
        void MovePlayerDirectlyToSpaceNumber(IPlayer player, int spaceNumber);
        void MoveToNearestRailroad(IPlayer player);
        void MoveToNearestUtility(IPlayer player, IDice dice);
        void HandlePurchasing(IPlayer player);
    }
}
