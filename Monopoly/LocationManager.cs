using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Locations
{
    public class LocationManager
    {
        private LocationFactory locationFactory;
        private const int NUMBER_OF_SPACES = 40;

        public LocationManager(Realtor realtor)
        {
            locationFactory = new LocationFactory(realtor);
        }

        public ILocation MovePlayer(IPlayer player, int distance)
        {
            int nextSpaceNumber = player.PlayerLocation.SpaceNumber + distance;

            while (nextSpaceNumber > 40) // Handles Flying over go
            {
                player.Balance += 200;

                nextSpaceNumber -= NUMBER_OF_SPACES;
            }
          
            return locationFactory.GetLocationForSpaceNumber(nextSpaceNumber % 40);
        }

        public ILocation MovePlayerDirectlyToSpaceNumber(IPlayer player, int spaceNumber)
        {
            ILocation nextLocation = locationFactory.GetLocationForSpaceNumber(spaceNumber);

            if (nextLocation.SpaceNumber < player.PlayerLocation.SpaceNumber)
            {
                nextLocation.AddOnLandTask(new LandOnGoTask());
            }

            return nextLocation;
        }

        public int ChompToBoardSize(int spaceNumber)
        {
            return spaceNumber % NUMBER_OF_SPACES;
        }

        public void MoveToClosest(IPlayer player, PropertyGroup desiredGroup)
        {
            ILocation closestLocationInGroup = locationFactory.GetClosest(player.PlayerLocation.SpaceNumber, desiredGroup);

            MovePlayerToLocation(player, closestLocationInGroup);
        }

        public void MovePlayerToLocation(IPlayer player, ILocation location)
        {
            player.CompleteExitLocationTasks();
            player.PlayerLocation = location;
            player.CompleteLandOnLocationTasks();
        }
    }
}
