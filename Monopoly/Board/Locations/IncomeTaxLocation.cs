using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Tasks;

namespace Monopoly.Locations
{
    public class IncomeTaxLocation : Location
    {
        public IncomeTaxLocation() : base(4, PropertyGroup.Tax)
        {
            OnLandTasks.Add(new PayIncomeTaxTask());
        }
    }
}
