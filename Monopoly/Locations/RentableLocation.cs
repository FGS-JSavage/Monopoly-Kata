using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Locations
{
    public class RentableLocation : Location
    {
        public int Price { get; set; }
        public int Rent { get; set; }

        public RentableLocation(int spaceNumber, int rent, int price, PropertyGroup group) : base(spaceNumber, group)
        {
            Rent = rent;
            Price = price;
        }
    }
}
