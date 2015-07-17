
namespace Monopoly.Board.Locations
{
    public class RentableLocation : Location
    {
        public int Price { get; set; }
        public int Rent { get; set; }

        public RentableLocation(int spaceNumber, int rent, int price, PropertyGroup group) : base(spaceNumber, group)
        {
            Rent = rent;
            Price = price;
        }
    }
}
