using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Tasks
{
    public class PayLuxuryTaxTask : IPlayerTask
    {
        public void Complete(IPlayer player)
        {
            player.Balance = player.Balance - 75 > 0 ? player.Balance - 75 : 0;
        }
    }
}
