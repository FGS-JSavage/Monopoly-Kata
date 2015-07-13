using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Locations
{
    public class JailLocation : Location
    {
        public JailStrategy strategy;
        public JailLocation() : base(10, PropertyGroup.Jail)
        {
            
        }
    }

    public enum JailStrategy
    {
        RollDoubles,
        Pay,
        UseGetOutOfJailCard
    }

}
