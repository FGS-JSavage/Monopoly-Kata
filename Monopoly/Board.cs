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

        private LocationFactory locationFactory;

        public Board()
        {
            locationFactory = new LocationFactory();
        }

        public ILocation MoveForward(ILocation location, int distance)
        {
            return locationFactory.GetLocationForSpaceNumber(location.GetSpaceNumber());
        }
    }
}
