using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public interface IMovementHandler
    {
        ILocation MovePlayer(IPlayer player, int distance);
        ILocation MovePlayerDirectlyToSpaceNumber(IPlayer player, int spaceNumber);
        void MoveToClosest(IPlayer player, PropertyGroup desiredGroup);
        void MovePlayerToLocation(IPlayer player, ILocation location);
    }
}
