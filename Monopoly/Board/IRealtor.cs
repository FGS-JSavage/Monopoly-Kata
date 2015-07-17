
using Monopoly.Board.Locations;

namespace Monopoly.Board
{
    public interface IRealtor
    {
        void MakePurchase(IPlayer player);
        void ChargeRent(IPlayer renter, int diceRollValue);
        void ChargeDoubleRailroadRent(IPlayer renter);
        void ChargeTenTimesRollValueRent(IPlayer player, int rollValue);
        int CalculateRent(int spaceNumber, int diceRollValue);
        bool SpaceIsOwned(int spaceNumber);
        bool IsWholeGroupOwned(PropertyGroup group);
        int CountOwnedPropertiesWithSameGroupAndOwner(int spaceNumber);
        int CountOwnedPropertiesWithSameGroup(int spaceNumber);
        bool SpaceIsForSale(int spaceNumber);
        int GetPriceOfSpace(int spaceNumber);
        int GetRentOfSpace(int spaceNumber);
        void SetOwnerForSpace(IPlayer player, int spaceNumber);
        ILocation LocationForSpaceNumber(int spaceNumber);
        IPlayer GetOwnerForSpace(int spaceNumber);
        ILocation GetClosest(int spaceNumber, PropertyGroup desiredGroup);
    }
}