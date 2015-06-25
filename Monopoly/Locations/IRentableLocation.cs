using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Locations
{
    interface IRentableLocation : ILocation
    {
        int Price { get; set; }
        int Rent  { get; set; }
    }
}
