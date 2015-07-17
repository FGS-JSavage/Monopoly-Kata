using System.Collections.Generic;
using Monopoly.Tasks;

namespace Monopoly.Board.Locations
{
    public interface ILocation
    {
        int SpaceNumber { get; set; }
        List<IPlayerTask> GetOnLandTasks();
        PropertyGroup Group { get; set; }
    }
}
