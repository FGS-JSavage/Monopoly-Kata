using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Practices.ObjectBuilder2;
using Monopoly.Locations;

namespace Monopoly
{
    public class Realtor
    {
        private Dictionary<int, IPlayer> ownersBySpaceNumber;
        private Dictionary<int, ILocation> propertyList;
        private Banker banker;

        public Realtor(Banker banker, out Dictionary<int, ILocation> propertyList)
        {
            this.banker = banker;
            this.propertyList = propertyList;
            ownersBySpaceNumber = new Dictionary<int, IPlayer>();
        }

        public void MakePurchase(IPlayer player, int spaceNumber)
        {
            banker.Collect(player, GetPriceOfSpace(spaceNumber));
            SetOwnerForSpace(player, spaceNumber);
        }

        public void ChargeRent(IPlayer owner, IPlayer renter, int diceRollValue)
        {
            var rentalRate = CalculateRent(renter.PlayerLocation.SpaceNumber, diceRollValue);
            banker.Transfer(renter, owner, rentalRate);
        }

        public virtual int CalculateRent(int spaceNumber, int diceRollValue)
        {
            var group = propertyList[spaceNumber].Group;

            if (group == PropertyGroup.Railroad) // Railroad Rent
            {
                return 25 * CountOwnedPropertiesWithSameGroupAndOwner(spaceNumber);
            }

            if (group == PropertyGroup.Utility) // Utility Rent
            {
                return ( CountOwnedPropertiesWithSameGroup(spaceNumber) == 1 ? 4 : 10 ) * diceRollValue;
            }

            // Real Estate Rent
            return ( IsWholeGroupOwned(group) ? 2 : 1 ) * ((RentableLocation)propertyList[spaceNumber]).Rent;
        }

        public bool SpaceIsOwned(int spaceNumber)
        {
            return ownersBySpaceNumber.ContainsKey(spaceNumber);
        }

        public bool IsWholeGroupOwned(PropertyGroup group)
        {
            bool AllAreOwned = true;
            foreach (ILocation i in propertyList.Values.Where(j => j is RentableLocation && j.Group == group))
            {
                AllAreOwned &= SpaceIsOwned(i.SpaceNumber);
            }
            return AllAreOwned;
        }

        public int CountOwnedPropertiesWithSameGroupAndOwner(int spaceNumber)
        {
            var group = propertyList[spaceNumber].Group;
            var owner = GetOwnerForSpace(spaceNumber);
            int propertiesAlsoOwnedBySamePlayer = 0;
            
            foreach (ILocation i in propertyList.Values.Where(j => j is RentableLocation && j.Group == group))
            {
                if (SpaceIsOwned(i.SpaceNumber) && GetOwnerForSpace(i.SpaceNumber) == owner)
                {
                    propertiesAlsoOwnedBySamePlayer++;
                }

            }
            return propertiesAlsoOwnedBySamePlayer;
        }

        public int CountOwnedPropertiesWithSameGroup(int spaceNumber)
        {
            var group = propertyList[spaceNumber].Group;

            int propertiesAlsoOwned = 0;

            foreach (ILocation i in propertyList.Values.Where(j => j is RentableLocation && j.Group == group))
            {
                if (SpaceIsOwned(i.SpaceNumber))
                {
                    propertiesAlsoOwned++;
                }
            }
            return propertiesAlsoOwned;
        }


        public bool SpaceIsForSale(int spaceNumber)
        {
            return propertyList[spaceNumber] is RentableLocation && !SpaceIsOwned(spaceNumber);
        }

        public int GetPriceOfSpace(int spaceNumber)
        {
            return ((RentableLocation)propertyList[spaceNumber]).Price;
        }

        public int GetRentOfSpace(IPlayer player, int spaceNumber)
        {
            return ((IRentableLocation) propertyList[spaceNumber]).Rent;
        }

        public void SetOwnerForSpace(IPlayer player, int spaceNumber)
        {
            ownersBySpaceNumber.Add(spaceNumber, player);
        }

        public ILocation LocationForSpaceNumber(int spaceNumber)
        {
            return propertyList[spaceNumber];
        }

        public IPlayer GetOwnerForSpace(int spaceNumber)
        {
            return ownersBySpaceNumber[spaceNumber];
        }
    }

    public enum PropertyGroup
    {
        Purple,
        LightGreen,
        Violet,
        Orange,
        Red,
        Yellow,
        DarkGreen,
        DarkBlue,
        Utility,
        Railroad,
        Jail,
        JailVisiting,
        Tax,
        Go,
        Chest,
        Chance,
        FreeParking
    }
}
