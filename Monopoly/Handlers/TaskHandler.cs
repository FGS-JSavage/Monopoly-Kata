using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Locations;

namespace Monopoly.Tasks
{
    public class TaskHandler : ITaskHandler
    {
        private IMovementHandler movementHandler;
        private List<IPlayer> players;
        private IBanker banker;
        private IJailer jailer;
        private IRealtor realtor;

        public TaskHandler(IRealtor realtor, List<IPlayer> players, IMovementHandler movementHandler, IBanker banker, IJailer jailer)
        {
            this.movementHandler = movementHandler;
            this.players = players;
            this.banker = banker;
            this.jailer = jailer;
            this.realtor = realtor;
        }

        public void HandleCollectFromAllPlayersTask(IPlayer player, int amount)
        {
            players.ForEach(x => banker.Transfer(x, player, amount));
        }

        public void HandleCollectFromBankerTask(IPlayer player, int amount)
        {
            banker.Payout(player, amount);
        }

        public void HandleDrawChest(IPlayer player)
        {
            // TODO remove this method
        }

        public void HandleMoveToLocationTask(IPlayer player, int spaceNumber)
        {
            movementHandler.MovePlayerDirectlyToSpaceNumber(player, spaceNumber);
        }

        public void HandlePayBankerTask(int amount, IPlayer player)
        {
            banker.Collect(player, amount);
        }

        public void MoveToClosest(IPlayer player, PropertyGroup group)
        {
            //movementHandler.MoveToClosest(player, group);
        }

        public void SendPlayerToJail(IPlayer player)
        {
            movementHandler.MovePlayerDirectlyToSpaceNumber(player, 30);
        }

        public void MoveDistance(int distance, IPlayer player)
        {
            movementHandler.MovePlayer(player, distance);
        }
    }
}
