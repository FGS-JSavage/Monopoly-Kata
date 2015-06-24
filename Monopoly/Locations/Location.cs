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

        public void AddOnLandTask(IPlayerTask task)
        {
            OnLandTasks.Add(task);
        }

        public List<IPlayerTask> GetOnLandTasks()
        {
            return OnLandTasks;
        }

        public List<IPlayerTask> GetOnExitTasks()
        {
            return OnExitTasks;
        } 
    }
}
