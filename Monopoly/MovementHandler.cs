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
          
            MovePlayerToLocation(player, realtor.LocationForSpaceNumber(ChompToBoardSize(nextSpaceNumber)));
        }

        public void MovePlayerDirectlyToSpaceNumber(IPlayer player, int spaceNumber)
        {
            ILocation nextLocation = realtor.LocationForSpaceNumber(spaceNumber);

            if (nextLocation.SpaceNumber < player.PlayerLocation.SpaceNumber)
            {
                //nextLocation.AddOnLandTask(new LandOnGoTask());
                //player.Balance += 200;
            }

            MovePlayerToLocation(player, nextLocation);
            
        }

        public void MoveToClosestUtility(IPlayer player, PropertyGroup desiredGroup)
        {
            ILocation closestRailroad = realtor.GetClosest(player.PlayerLocation.SpaceNumber, PropertyGroup.Railroad);

            MovePlayerToLocation(player, closestRailroad);
        }

        public void MoveToClosestRailroad(IPlayer player, IDice dice)
        {
            MoveToClosestUtility(player, PropertyGroup.Railroad);

            HandleLanding(player, dice, 2);
        }

        public void MoveToClosestUtility(IPlayer player, IDice dice)
        {
            MoveToClosestUtility(player, PropertyGroup.Railroad);

            HandleLanding(player, dice, 10);
        }

        public void MovePlayerToLocation(IPlayer player, ILocation location)
        {
            player.CompleteExitLocationTasks();
            player.PlayerLocation = location;
            player.CompleteLandOnLocationTasks();
        }

        public void MoveDirectlyToJail(IPlayer player)
        {
            throw new NotImplementedException();
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

        public void HandleLanding(IPlayer player, IDice dice, int multiplier)
        {
            if (realtor.SpaceIsForSale(player.PlayerLocation.SpaceNumber))
            {
                realtor.MakePurchase(player, player.PlayerLocation.SpaceNumber);
            }
            else if (realtor.SpaceIsOwned(player.PlayerLocation.SpaceNumber)) // then it must be owned
            {
                dice.Roll();
                realtor.ChargeRent(realtor.GetOwnerForSpace(player.PlayerLocation.SpaceNumber), player, dice.Score, multiplier);
            }
        }

        public int ChompToBoardSize(int spaceNumber)
        {
            return (spaceNumber + NUMBER_OF_SPACES) % NUMBER_OF_SPACES;
        }
    }
}