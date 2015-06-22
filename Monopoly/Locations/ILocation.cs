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
        void MoveFowrard(int disatnce);
        int GetSpaceNumber();
        void Land();
        void Exit();
    }

}
