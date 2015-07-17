
namespace Monopoly.Board.Locations
{
    interface IRentableLocation : ILocation
    {
        int Price { get; set; }
        int Rent  { get; set; }
    }
}
