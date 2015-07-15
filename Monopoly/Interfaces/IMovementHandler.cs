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
        void MoveToClosestRailroad(IPlayer player, IDice dice);
        void MoveToClosestUtility(IPlayer player);
        void MovePlayerToLocation(IPlayer player, ILocation location);
        void MoveDirectlyToJail(IPlayer player);
        void HandleLanding(IPlayer player, int distance);
    } 
}
