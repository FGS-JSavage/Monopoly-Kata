using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class MovementHandler : IMovementHandler
    {
        private IRealtor realtor;
        private const int NUMBER_OF_SPACES = 40;

        public MovementHandler(IRealtor realtor)
        {
            this.realtor = realtor;
        }

        

        public void MovePlayer(IPlayer player, int distance)
        {
            int nextSpaceNumber = player.PlayerLocation.SpaceNumber + distance;

            while (nextSpaceNumber > 40) // Handles Flying over go
            {
                player.Balance += 200;

                nextSpaceNumber -= NUMBER_OF_SPACES;
            }
          
            

            MovePlayerToLocation(player, realtor.LocationForSpaceNumber(nextSpaceNumber % 40));
        }

        public void MovePlayerDirectlyToSpaceNumber(IPlayer player, int spaceNumber)
        {
            ILocation nextLocation = realtor.LocationForSpaceNumber(spaceNumber);

            if (nextLocation.SpaceNumber < player.PlayerLocation.SpaceNumber)
            {
                nextLocation.AddOnLandTask(new LandOnGoTask());
            }

            MovePlayerToLocation(player, nextLocation);
            
        }

        public int ChompToBoardSize(int spaceNumber)
        {
            return spaceNumber % NUMBER_OF_SPACES;
        }

        public void MoveToClosest(IPlayer player, PropertyGroup desiredGroup)
        {
            ILocation closestLocationInGroup = realtor.GetClosest(player.PlayerLocation.SpaceNumber, desiredGroup);

            MovePlayerToLocation(player, closestLocationInGroup);
        }

        public void MovePlayerToLocation(IPlayer player, ILocation location)
        {
            player.CompleteExitLocationTasks();
            player.PlayerLocation = location;
            player.CompleteLandOnLocationTasks();
        }

        public void HandleLanding(IPlayer player, int distance)
        {
            if (realtor.SpaceIsForSale(player.PlayerLocation.SpaceNumber))
            {
                realtor.MakePurchase(player, player.PlayerLocation.SpaceNumber);
            }
            else if (realtor.SpaceIsOwned(player.PlayerLocation.SpaceNumber)) // then it must be owned
            {
                realtor.ChargeRent(realtor.GetOwnerForSpace(player.PlayerLocation.SpaceNumber), player, distance);
            }
        }

        public delegate void HandleMoveToClosestLocation(IPlayer player);


    }
}