using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Tasks;

namespace Monopoly.Locations
{
    public class ChanceLocation : Location
    {
        public ChanceLocation(int spaceNumber, PropertyGroup @group, Board board) : base(spaceNumber, @group)
        {
            OnLandTasks.Add(new DrawChanceTask(board));
        }
    }
}
