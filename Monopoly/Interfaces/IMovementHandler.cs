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
        void MoveToNearestRailroad(IPlayer player);
        void MoveToNearestUtility(IPlayer player, IDice dice);
        void MoveDirectlyToJail(IPlayer player);
        void HandlePurchasing(IPlayer player);
    } 
}
