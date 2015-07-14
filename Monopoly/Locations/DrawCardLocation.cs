using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Tasks;

namespace Monopoly.Locations
{
    public class DrawCardLocation : Location
    {
        private IDeck deck;

        public DrawCardLocation(int spaceNumber, IDeck deck, PropertyGroup @group) : base(spaceNumber, @group)
        {
            this.deck = deck;
        }

        public void AddDeck(IDeck deck)
        {
            this.deck = deck;
            OnLandTasks.Add(new DrawCardTask(deck));
        }
    }
}
