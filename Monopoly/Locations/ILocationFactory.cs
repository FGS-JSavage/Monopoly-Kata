using System.Collections.Generic;

namespace Monopoly.Locations
{
    public interface ILocationFactory
    {
        Dictionary<int, ILocation> BuildLocations();
        void InjectDecks(IDeck chanceDeck, IDeck chestDeck);
    }
}