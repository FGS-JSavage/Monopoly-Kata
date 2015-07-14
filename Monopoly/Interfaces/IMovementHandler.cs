using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public interface IMovementHandler
    {
        void MovePlayer(IPlayer player, int distance);
        void MovePlayerDirectlyToSpaceNumber(IPlayer player, int spaceNumber);
        void MoveToClosest(IPlayer player, PropertyGroup desiredGroup);
        void MovePlayerToLocation(IPlayer player, ILocation location);
        void HandleLanding(IPlayer player, int distance);
    } 
}
