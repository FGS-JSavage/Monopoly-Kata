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

        public LocationManager()
        {
            locationFactory = new LocationFactory();
        }

        public void MovePlayer(IPlayer player, int distance)
        {

            List<LandOnGoTask> onLandTasksToAdd = new List<LandOnGoTask>();

            int nextSpaceNumber = player.PlayerLocation.SpaceNumber + distance;

            while (nextSpaceNumber > 40) // Handles Flying over go
            {
                onLandTasksToAdd.Add(new LandOnGoTask());
                nextSpaceNumber -= NUMBER_OF_SPACES;
    
            }
          
            player.CompleteExitLocationTasks();
            player.PlayerLocation = locationFactory.GetLocationForSpaceNumber(nextSpaceNumber % 40);
            onLandTasksToAdd.ForEach(task => player.PlayerLocation.AddOnLandTask(task));
            player.CompleteLandOnLocationTasks();
        }

        public int ChompToBoardSize(int spaceNumber)
        {
            return spaceNumber % NUMBER_OF_SPACES;
        } 
    }
}
