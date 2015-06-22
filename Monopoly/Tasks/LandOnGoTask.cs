using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class LandOnGoTask : IPlayerTask
    {
        public void Complete(IPlayer player)
        {
            player.Balance += 200;
        }
    }
}
