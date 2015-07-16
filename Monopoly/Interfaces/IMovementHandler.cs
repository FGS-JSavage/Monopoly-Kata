
namespace Monopoly
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
