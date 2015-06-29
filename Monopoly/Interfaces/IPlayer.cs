using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Locations;

namespace Monopoly
{
    public interface IPlayer
    {
        ILocation PlayerLocation    { get; set; }
        double Balance              { get; set; }
        int DoublesCount            { get; set; }

        void CompleteLandOnLocationTasks();
        void CompleteExitLocationTasks();

        void UseGetOutOfJailCard();
        bool HasGetOutOfJailCard();

        JailStrategy GetJailStrategy();
    }
}
