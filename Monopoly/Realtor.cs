using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Locations;

namespace Monopoly
{
    public class Realtor
    {
        private Dictionary<int, IPlayer> ownersBySpaceNumber;
        private Dictionary<int, ILocation> propertyList;
        
        public Realtor()
        {
            ownersBySpaceNumber = new Dictionary<int, IPlayer>();
            propertyList = new Dictionary<int, ILocation>()
            {
                { 0, new GoLocation()               },
                { 1, new RentableLocation(1, 2, 60) },
                // TODO Electric company Location
                { 3, new RentableLocation(3, 60, 4) },
                { 6, new RentableLocation(, , ) },
                { 1, new RentableLocation(, , ) },
                { 1, new RentableLocation(, , ) },
                { 1, new RentableLocation(, , ) },
                { 1, new RentableLocation(, , ) },
                { 1, new RentableLocation(, , ) },
                { 1, new RentableLocation(, , ) },
                { 1, new RentableLocation(, , ) },
                { 1, new RentableLocation(, , ) },
            }
        }

        public bool SpaceIsOwned(int spaceNumber)
        {
            return ownersBySpaceNumber[spaceNumber] != null;
        }

        public IPlayer GetOwnerForSpaceNumber(int spaceNumber)
        {
            return ownersBySpaceNumber[spaceNumber];
        }
    }

    public enum PropertyType
    {
        Purple,
        LightGreen,
        Violet,
        Orange,
        Red,
        Yellow,
        DarkGreen,
        DarkBlue,
        Utility,
        Railroad,
        Jail,
        Tax,
        Go
    }
}
