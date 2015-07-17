using System.Collections.Generic;
using Monopoly.Tasks;

namespace Monopoly.Board.Locations
{
    public class Location : ILocation
    {
        protected List<IPlayerTask> OnLandTasks = new List<IPlayerTask>();

        public int SpaceNumber     { get; set; }
        public PropertyGroup Group { get; set; }

        public Location(int spaceNumber, PropertyGroup group)
        {
            SpaceNumber = spaceNumber;
            Group = group;
        }

        public List<IPlayerTask> GetOnLandTasks()
        {
            return OnLandTasks;
        }
    }
}
