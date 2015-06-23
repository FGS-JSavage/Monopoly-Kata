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
        public LocationManager locationManager { get; }

        public Board()
        {
            dice = new Dice();
            locationManager = new LocationManager();
        }

        public void DoTurn(IPlayer player)
        {
            player.CompletePreMoveTasks();
            locationManager.MovePlayer(player, dice.Roll());
            player.CompletePostMoveTasks();
        }
    }
}
