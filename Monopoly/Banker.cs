using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class Banker
    {
        public Banker()
        {
            
        }

        public void ChargePlayerToGetOutOfJail(IPlayer player)
        {
            player.Balance -= 50;
        }

    }
}
