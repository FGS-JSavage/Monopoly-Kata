using System.Collections.Generic;

namespace Monopoly.Locations
{
    public interface ILocationFactory
    {
        Dictionary<int, ILocation> BuildLocations();
    }
}