
using Monopoly.Tasks;

namespace Monopoly.Board.Locations
{
    public class GoLocation : Location
    {
        public GoLocation() : base(0, PropertyGroup.Go)
        {
            OnLandTasks.Add(new LandOnGoTask());
        }
    }
}
