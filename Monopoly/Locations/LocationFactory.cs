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
        private Realtor realtor;

        public LocationFactory(Realtor realtor)
        {
            this.realtor = realtor;

            locationKeeper = new Dictionary<int, ILocation>()
            {
                { 0, new GoLocation() }
            };
        }

        public ILocation GetLocationForSpaceNumber(int spaceNumber)
        {
            return realtor.LocationForSpaceNumber(spaceNumber);

            //if (spaceNumber == 0)
            //{
            //    return new GoLocation();
            //}

            //if (spaceNumber == 4)
            //{
            //    return new IncomeTaxLocation();
            //}

            //if (spaceNumber == 30)
            //{
            //    return new JailVisitingLocation();
            //}

            //if (spaceNumber == 38)
            //{
            //    return new LuxuryTaxLocation();
            //}
            

            //return new Location(spaceNumber, PropertyGroup.FreeParking);
        }
    }
}
