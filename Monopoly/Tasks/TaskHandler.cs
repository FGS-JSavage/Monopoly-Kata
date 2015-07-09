using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Locations;

namespace Monopoly.Tasks
{
    public class TaskHandler
    {
        private IMovementHandler movementHandler;
        private List<IPlayer> players;
        private IBanker banker;
        private IJailer jailer;

        public TaskHandler(IMovementHandler movementHandler, List<IPlayer> players, IBanker banker, IJailer jailer)
        {
            this.movementHandler = movementHandler;
            this.players = players;
            this.banker = banker;
            this.jailer = jailer;
        }

        public void HandleLandOnGoTask()
        {
                
        }

        public void HandleCollectFromAllPlayersTask(IPlayer player, int amount)
        {
            players.ForEach(x => banker.Transfer(x, player, amount));
        }

        public void HandleCollectFromBankerTask(IPlayer player, int amount)
        {
            banker.Payout(amount);
        }

        public void HandleDrawChest(IPlayer player)
        {
            
        }

        public void HandleMoveToLocationTask(IPlayer player, int spaceNumber)
        {
            movementHandler.MovePlayerDirectlyToSpaceNumber(player, spaceNumber);
        }

        public void HandlePayBankerTask(int amount, IPlayer player)
        {
            banker.Collect(player, amount);
        }

        public void MoveToClosest(IPlayer player, PropertyGroup utility)
        {
            movementHandler.MoveToClosest();
        }
    }
}
