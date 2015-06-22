using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Locations
{
    public class LocationFactory
    {
         List<KeyValuePair<int, ILocation>> locationKeeper;

        public LocationFactory()
        {
            locationKeeper = new List<KeyValuePair<int, ILocation>>();

            locationKeeper.Add(new KeyValuePair<int, ILocation>(0, new ));
        }

        public ILocation GetLocationForSpaceNumber(int spaceNumber) 
        {
            return 
        }
    }
}
