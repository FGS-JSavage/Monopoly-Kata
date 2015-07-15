namespace Monopoly.Tasks
{
    public interface ITaskHandler
    {
        void HandleCollectFromAllPlayersTask(IPlayer player, int amount);
        void HandleCollectFromBankerTask(IPlayer player, int amount);
        void HandleDrawChest(IPlayer player);
        void HandleMoveToLocationTask(IPlayer player, int spaceNumber);
        void HandlePayBankerTask(int amount, IPlayer player);
        void MoveToClosest(IPlayer player, PropertyGroup group);
        void SendPlayerToJail(IPlayer player);
        void MoveDistance(int distance, IPlayer player);
        void HandlePayAllOtherPlayers(IPlayer player, int amount);
    }
}