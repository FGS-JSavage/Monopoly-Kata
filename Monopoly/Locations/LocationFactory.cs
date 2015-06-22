using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Locations
{
    public class LocationFactory
    {
        Dictionary<int, ILocation> locationKeeper;

        public LocationFactory()
        {
            locationKeeper = new Dictionary<int, ILocation>()
            {
                { 0, new GoLocation() }
            };
        }

        public ILocation GetLocationForSpaceNumber(int spaceNumber) 
        {

            if (locationKeeper[spaceNumber] != null)
            {
                return locationKeeper[spaceNumber];
            }
            else // Falls through to give generic non-rentable space TODO get rid of this
            {
                return new Location();
            }
        }
    }
}
