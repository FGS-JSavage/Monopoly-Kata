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
        private Realtor realtor;
        private Banker banker;

        public Board()
        {
            dice = new Dice();
            realtor = new Realtor();
            locationManager = new LocationManager(realtor);
            banker = new Banker(realtor);

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

            if (realtor.SpaceIsForSale(player.PlayerLocation.SpaceNumber))
            {
                realtor.MakePurchase(player, player.PlayerLocation.SpaceNumber);
            }
            else if (realtor.SpaceIsOwned(player.PlayerLocation.SpaceNumber)) // then it must be owned
            {
                realtor.ChargeRent(realtor.GetOwnerForSpace(player.PlayerLocation.SpaceNumber), player);
            }
        }

        public LocationManager GetLocationManager()
        {
            return locationManager;
        }

        public Realtor GetRealtor()
        {
            return realtor;
        }
    }
}
