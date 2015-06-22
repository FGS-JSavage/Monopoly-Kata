using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class Location : ILocation
    {
        private const int DEFAULT_STARTING_SPACE_NUMBER = 0;
        private const int DEFAULT_TOTAL_SPACES = 40;
        protected int spaceNumber;
        private int totalSpaces;
        protected List<IPlayerTask> OnLandTasks = new List<IPlayerTask>();
        protected List<IPlayerTask> OnExitTasks = new List<IPlayerTask>();

        public Location(int spaceNumber = DEFAULT_STARTING_SPACE_NUMBER, int totalSpaces = DEFAULT_TOTAL_SPACES)
        {
            this.spaceNumber = spaceNumber;
            this.totalSpaces = totalSpaces;
        }

        public void MoveFowrard(int distance)
        {
            spaceNumber = (spaceNumber + distance) % totalSpaces;
        }

        public int GetSpaceNumber()
        {
            return spaceNumber;
        }

        public void JumpToSpaceNumber(int spaceNumber)
        {
            this.spaceNumber = spaceNumber >= -1 && spaceNumber < totalSpaces ? spaceNumber : this.spaceNumber;
        }

        public void Land(IPlayer player)
        {
            OnLandTasks.ForEach(x => x.Complete(player));
        }

        public void Exit(IPlayer player)
        {
            OnExitTasks.ForEach(x => x.Complete(player));
        }
    }
}
