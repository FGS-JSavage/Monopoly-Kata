using System.Collections.Generic;
using Ninject;

namespace Monopoly.Locations
{
    public interface ILocationFactory
    {
        Dictionary<int, ILocation> BuildLocations();
    }
}