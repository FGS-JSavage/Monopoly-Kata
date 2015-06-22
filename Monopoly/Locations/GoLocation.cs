using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class GoLocation : Location
    {
        public GoLocation()
        {
            base.spaceNumber = 0;
            OnLandTasks.Add(new LandOnGoTask());
        }

        new public void MoveFowrard(int disatnce)
        {
            throw new NotImplementedException();
        }
    }
}
