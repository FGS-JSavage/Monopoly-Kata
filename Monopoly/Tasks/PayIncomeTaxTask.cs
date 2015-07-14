using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Tasks
{
    public class PayIncomeTaxTask : IPlayerTask
    {
        public void Complete(IPlayer player)
        {
            player.Balance -= player.Balance * 0.1 < 200 ? player.Balance * 0.1 : 200;
        }
    }
}
