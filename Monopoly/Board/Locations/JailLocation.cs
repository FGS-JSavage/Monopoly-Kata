
using Monopoly.Player;

namespace Monopoly.Board.Locations
{
    public class JailLocation : Location
    {
        public JailStrategy strategy;
        public JailLocation() : base(30, PropertyGroup.Jail)
        {
            
        }
    }
}
