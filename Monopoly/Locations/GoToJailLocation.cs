using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Tasks;

namespace Monopoly.Locations
{
    class GoToJailLocation : Location
    {
        public GoToJailLocation() : base(30, PropertyGroup.Jail)
        {
            //OnLandTasks.Add();
        }
    }
}