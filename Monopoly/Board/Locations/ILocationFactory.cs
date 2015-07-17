using System.Collections.Generic;

namespace Monopoly.Board.Locations
{
    public interface ILocationFactory
    {
        Dictionary<int, ILocation> BuildLocations();
    }
}