using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public interface ICard
    {
        string Name { get; set; }
        List<IPlayerTask> Tasks { get; set; }
    }
}
