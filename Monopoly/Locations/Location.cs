using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class Location : ILocation
    {
        protected List<IPlayerTask> OnLandTasks = new List<IPlayerTask>();
        protected List<IPlayerTask> OnExitTasks = new List<IPlayerTask>();

        public int SpaceNumber { get; set; }

        public Location(int spaceNumber)
        {
            this.SpaceNumber = spaceNumber;
        }

        public void Land(IPlayer player)
        {
            OnLandTasks.ForEach(x => x.Complete(player));
        }

        public void Exit(IPlayer player)
        {
            OnExitTasks.ForEach(x => x.Complete(player));
        }

        public void AddOnLandTask(IPlayerTask task)
        {
            OnLandTasks.Add(task);
        }
    }
}
