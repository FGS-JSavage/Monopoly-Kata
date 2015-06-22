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
        private const int NUMBER_OF_SPACES = 40;

        public Board()
        {
            locationFactory = new LocationFactory();
        }

        public ILocation MoveToSpace(int spaceNumber)
        {
            return locationFactory.GetLocationForSpaceNumber(spaceNumber);
        }

        public int GetNextSpaceNumber(int distance, int spaceNumber)
        {
            return (spaceNumber + distance) % NUMBER_OF_SPACES;
        }

    }
}
