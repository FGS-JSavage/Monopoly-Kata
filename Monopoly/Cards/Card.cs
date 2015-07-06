using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class Card : ICard
    {
        public String Name      { get; set; }
        public List<IPlayerTask> Tasks { get; set; }

        public Card(string name, List<IPlayerTask> tasks)
        {
            Name = name;
            Tasks = tasks;
        }

        public Card(string name, IPlayerTask task)
        {
            Name = name;
            Tasks = new List<IPlayerTask>()
            {
                {task}
            };
        }
    }
}
