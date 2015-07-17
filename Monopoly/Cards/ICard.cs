using System.Collections.Generic;
using Monopoly.Tasks;

namespace Monopoly.Cards
{
    public interface ICard
    {
        string Name { get; set; }
        List<IPlayerTask> Tasks { get; set; }
        DeckType Type { get; set; }
    }
}
