using System.Collections.Generic;
using Ninject;

namespace Monopoly.Locations
{
    public interface ILocationFactory : IInitializable
    {
        Dictionary<int, ILocation> BuildLocations();
    }
}