namespace Monopoly.Tasks
{
    public interface ITaskHandler
    {
        void HandleCollectFromAllPlayersTask(IPlayer player, int amount);
        void HandleCollectFromBankerTask(IPlayer player, int amount);
        void HandleMoveToLocationTask(IPlayer player, int spaceNumber);
        void HandlePayBankerTask(int amount, IPlayer player);
        void HandleGoDirectlyToJail(IPlayer player);
        void HandleMoveDistance(int distance, IPlayer player);
        void HandleMoveToNearestUtility(IPlayer player);
        void HandleMoveToNearestRailroad(IPlayer player);
        void HandlePayAllOtherPlayers(IPlayer player, int amount);
    }
}