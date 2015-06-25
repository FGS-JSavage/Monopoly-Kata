using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public interface ILocation
    {
        int SpaceNumber { get; set; }
        void AddOnLandTask(IPlayerTask task);
        List<IPlayerTask> GetOnLandTasks();
        List<IPlayerTask> GetOnExitTasks();
        PropertyGroup Group { get; set; }
    }
}
