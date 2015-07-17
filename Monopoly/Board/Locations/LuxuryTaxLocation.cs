using Monopoly.Tasks;

namespace Monopoly.Board.Locations
{
   public  class LuxuryTaxLocation : Location
    {
       public LuxuryTaxLocation() : base(38, PropertyGroup.Tax)
       {
           OnLandTasks.Add(new PayLuxuryTaxTask());
       }
    }
}
