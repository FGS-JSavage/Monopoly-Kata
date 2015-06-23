using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Locations
{
    public class JailVisitingLocation : ILocation
    {
        public void MoveFowrard(int disatnce)
        {
            throw new NotImplementedException();
        }

        public int GetSpaceNumber()
        {
            throw new NotImplementedException();
        }

        public int SpaceNumber
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public void Land(IPlayer player)
        {
            // TODO
        }

        public void Exit(IPlayer player)
        {
            // TODO
        }

        public void AddOnLandTask(IPlayerTask task)
        {
            throw new NotImplementedException();
        }
    }
}
