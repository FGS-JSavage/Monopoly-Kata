using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class Player : IPlayer
    {
        private ILocation location;
        
        public Player(ILocation location)
        {
            this.location = location;
        }

        public void MoveDistance(int distance)
        {
            location.MoveFowrard(distance);
        }

        public ILocation GetLocation()
        {
            return location;
        }
    }
}
