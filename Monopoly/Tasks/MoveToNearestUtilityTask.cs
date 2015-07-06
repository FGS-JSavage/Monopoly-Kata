using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Tasks
{
    public class MoveToNearestUtilityTask : IPlayerTask
    {
        private Board board;
        int[] utilityLocations = new int[5] { 5, 7, 8, 15, 20 };  

        public MoveToNearestUtilityTask(Board board)
        {
            this.board = board;
        }


        public void Complete(IPlayer player)
        {
            int oldSpaceNumber = player.PlayerLocation.SpaceNumber;

            // http://stackoverflow.com/questions/10120944/c-sharp-finding-nearest-number-in-array
            var nearest = utilityLocations.OrderBy(x => Math.Abs((long)x - oldSpaceNumber)).First();
            
        }
    }
}
