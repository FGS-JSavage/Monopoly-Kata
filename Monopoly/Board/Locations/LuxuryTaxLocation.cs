using Monopoly.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Locations
{
   public  class LuxuryTaxLocation : Location
    {
       public LuxuryTaxLocation() : base(38, PropertyGroup.Tax)
       {
           OnLandTasks.Add(new PayLuxuryTaxTask());
       }
    }
}
