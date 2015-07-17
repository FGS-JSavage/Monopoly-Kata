using System;
using System.Collections.Generic;
using Monopoly.Tasks;

namespace Monopoly.Cards
{
    public class Card : ICard
    {
        public String Name             { get; set; }
        public List<IPlayerTask> Tasks { get; set; }
        public virtual DeckType Type   { get; set; }

        public Card(string name, IPlayerTask task, DeckType deckType)
        {
            Name = name;
            Tasks = new List<IPlayerTask>(){ {task} };
            Type = deckType;
        }
    }
}
