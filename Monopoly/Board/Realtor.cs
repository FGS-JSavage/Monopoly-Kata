using System.Collections.Generic;
using System.Linq;
using System;
using Monopoly.Board.Locations;
using Monopoly.Player;

namespace Monopoly.Board
{
    public class Realtor : IRealtor
    {
        private const int RAILROAD_RENT_PER = 25;

        private Dictionary<int, IPlayer> ownersBySpaceNumber;
        private Dictionary<int, ILocation> propertyList;
        private IBanker banker;

        public Realtor(IBanker banker, ILocationFactory locationFactory)
        {
            this.banker = banker;
            ownersBySpaceNumber = new Dictionary<int, IPlayer>();
            propertyList = locationFactory.BuildLocations();
        }

        public virtual void MakePurchase(IPlayer player)
        {
            var spaceNumber = player.PlayerLocation.SpaceNumber;
            var price = GetPriceOfSpace(spaceNumber);

            Charge(player, price);
            SetOwnerForSpace(player, spaceNumber);
        }

        public void ChargeRent(IPlayer renter, int diceRollValue)
        {
            var owner = GetOwnerForSpace(renter.PlayerLocation.SpaceNumber);
            var rentalRate = CalculateRent(renter.PlayerLocation.SpaceNumber, diceRollValue);
            Transfer(renter, owner, rentalRate);
        }

        public void ChargeTenTimesRollValueRent(IPlayer renter, int rollValue)
        {
            Transfer(renter, GetOwnerForSpace(renter), 10 * rollValue);
        }

        public void ChargeDoubleRailroadRent(IPlayer renter)
        {
            var owner = GetOwnerForSpace(renter.PlayerLocation.SpaceNumber);
            var rentalRate = RAILROAD_RENT_PER * CountOwnedPropertiesWithSameGroupAndOwner(renter.PlayerLocation.SpaceNumber);
            Transfer(renter, owner, rentalRate * 2);
        }

        private void Transfer(IPlayer renter, IPlayer owner, int amount)
        {
            banker.Transfer(renter, owner, amount);
        }

        private void Charge(IPlayer player, int amount)
        {
            banker.Collect(player, amount);
        }

        public virtual int CalculateRent(int spaceNumber, int diceRollValue)
        {
            var group = propertyList[spaceNumber].Group;

            if (group == PropertyGroup.Railroad) // Railroad Rent
            {
                return RAILROAD_RENT_PER * CountOwnedPropertiesWithSameGroupAndOwner(spaceNumber);
            }

            if (group == PropertyGroup.Utility) // Utility Rent
            {
                return (CountOwnedPropertiesWithSameGroup(spaceNumber) == 1 ? 4 : 10 ) * diceRollValue;
            }

            // Real Estate Rent
            return ( IsWholeGroupOwned(group) ? 2 : 1 ) * GetRentOfSpace(spaceNumber);
        }

        public bool SpaceIsOwned(int spaceNumber)
        {
            return ownersBySpaceNumber.ContainsKey(spaceNumber);
        }

        private bool IsWholeGroupOwned(PropertyGroup group)
        {
            bool AllAreOwned = true;
            foreach (ILocation i in propertyList.Values.Where(j => j is RentableLocation && j.Group == group))
            {
                AllAreOwned &= SpaceIsOwned(i.SpaceNumber);
            }
            return AllAreOwned;
        }

        private int CountOwnedPropertiesWithSameGroupAndOwner(int spaceNumber)
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

        private int CountOwnedPropertiesWithSameGroup(int spaceNumber)
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

        public int GetRentOfSpace(int spaceNumber)
        {
            return ((RentableLocation) propertyList[spaceNumber]).Rent;
        }

        public void SetOwnerForSpace(IPlayer player, int spaceNumber)
        {
            ownersBySpaceNumber.Add(spaceNumber, player);
        }

        public ILocation LocationForSpaceNumber(int spaceNumber)
        {
            return propertyList[spaceNumber];
        }

        public IPlayer GetOwnerForSpace(IPlayer player)
        {
            return GetOwnerForSpace(player.PlayerLocation);
        }

        public IPlayer GetOwnerForSpace(ILocation location)
        {
            return GetOwnerForSpace(location.SpaceNumber);
        }

        public IPlayer GetOwnerForSpace(int spaceNumber)
        {
            return ownersBySpaceNumber[spaceNumber];
        }

        public ILocation GetClosest(int spaceNumber, PropertyGroup desiredGroup)
        {
            return propertyList.Values.Where(x => x.Group == desiredGroup)
                                        .OrderBy(y => Math.Abs(y.SpaceNumber - spaceNumber))
                                        .ThenByDescending(x => x.SpaceNumber)
                                        .First();
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
