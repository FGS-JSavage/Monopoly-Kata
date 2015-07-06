using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Cards
{
    class GetOutOfJailCard : Card
    {
        public GetOutOfJailCard(string name, List<IPlayerTask> tasks) : base(name, tasks)
        {
            
        }

        public GetOutOfJailCard(string name, IPlayerTask task) : base(name, task)
        {

        }
    }
}
