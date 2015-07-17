using Monopoly.Tasks;

namespace Monopoly.Board.Locations
{
    public class IncomeTaxLocation : Location
    {
        public IncomeTaxLocation() : base(4, PropertyGroup.Tax)
        {
            OnLandTasks.Add(new PayIncomeTaxTask());
        }
    }
}
