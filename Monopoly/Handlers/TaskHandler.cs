using System.Collections.Generic;
using Monopoly.Board;
using Monopoly.Tasks;

namespace Monopoly.Handlers
{
    public class TaskHandler : ITaskHandler
    {
        private IMovementHandler movementHandler;
        private List<IPlayer> players;
        private IBanker banker;
        private IDice dice;

        public TaskHandler(IRealtor realtor, List<IPlayer> players, IMovementHandler movementHandler, IBanker banker, IDice dice)
        {
            this.movementHandler = movementHandler;
            this.players = players;
            this.banker = banker;
            this.dice = dice;
        }

        public void HandleCollectFromAllPlayersTask(IPlayer player, int amount)
        {
            players.ForEach(x => banker.Transfer(x, player, amount));
        }

        public void HandleCollectFromBankerTask(IPlayer player, int amount)
        {
            banker.Payout(player, amount);
        }

        public void HandleMoveToLocationTask(IPlayer player, int spaceNumber)
        {
            movementHandler.MovePlayerDirectlyToSpaceNumber(player, spaceNumber);
        }

        public void HandlePayBankerTask(int amount, IPlayer player)
        {
            banker.Collect(player, amount);
        }

        public void HandleMoveToNearestRailroad(IPlayer player)
        {
            movementHandler.MoveToNearestRailroad(player);
        }

        public void HandleMoveToNearestUtility(IPlayer player)
        {
            movementHandler.MoveToNearestUtility(player, dice);
        }

        public void HandleGoDirectlyToJail(IPlayer player)
        {
            movementHandler.MovePlayerDirectlyToSpaceNumber(player, 30);
        }

        public void HandleMoveDistance(int distance, IPlayer player)
        {
            movementHandler.MovePlayer(player, distance);
        }

        public void HandlePayAllOtherPlayers(IPlayer player, int amount)
        {
            players.ForEach(x =>
            {
                x.Balance += amount;
                player.Balance -= amount;
            });
        }
    }
}
