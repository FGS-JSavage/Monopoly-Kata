using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Locations;

namespace Monopoly
{
    public class Board
    {
        private Dice dice;
        private LocationManager locationManager;

        public Board()
        {
            dice = new Dice();
            locationManager = new LocationManager();
        }

        public void DoTurn(IPlayer player)
        {
            DoTurn(player, dice.Roll());
        }

        public void DoTurn(IPlayer player, int distance)
        {
            player.CompleteExitLocationTasks();
            
            player.PlayerLocation = locationManager.MovePlayer(player, distance);
            
            player.CompleteLandOnLocationTasks();
        }



        public LocationManager GetLocationManager()
        {
            return locationManager;
        }
    }
}
