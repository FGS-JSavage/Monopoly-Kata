using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Locations
{
    public class JailVisitingLocation : ILocation
    {
        protected List<IPlayerTask> OnLandTasks = new List<IPlayerTask>();
        protected List<IPlayerTask> OnExitTasks = new List<IPlayerTask>();

        public void MoveFowrard(int disatnce)
        {
            throw new NotImplementedException();
        }

        public int GetSpaceNumber()
        {
            throw new NotImplementedException();
        }

        public int SpaceNumber { get; set; }

        public void AddOnLandTask(IPlayerTask task)
        {
            throw new NotImplementedException();
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
