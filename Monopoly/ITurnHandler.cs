namespace Monopoly
{
    public interface ITurnHandler
    {
        void DoTurn(IPlayer player);
        void DoTurn(IPlayer player, int distance, bool rolledDoubles);
        void DoJailTurn(IPlayer player, int distance, bool rolledDoubles);
        void DoStandardTurn(IPlayer player, int distance, bool RolledDoubles);
        void PayJailFine(IPlayer player);
        void HandleGetOutOfJailUsingCardStrategy(IPlayer player);
        void MovePlayerDirectlyToSpace(IPlayer player, int spaceNumber);
        void MoveToClosest(IPlayer player, PropertyGroup destinationGroup);
        void HandleGetOutOfJailByRollingDoublesStrategy(IPlayer player, int distance, bool rolledDoubles);
        void HandleGetOutOfJailByPaying(IPlayer player);
        void SendPlayerToJail(IPlayer player);
        void ReleasePlayerFromJail(IPlayer player);
        IMovementHandler GetLocationManager();
        IRealtor GetRealtor();
        IJailer GetJailer();
    }
}