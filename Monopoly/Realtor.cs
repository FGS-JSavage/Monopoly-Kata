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
        private Board board;

        public Realtor(Banker banker)
        {
            this.banker = banker;
            this.board = board;
            ownersBySpaceNumber = new Dictionary<int, IPlayer>();

            propertyList = new Dictionary<int, ILocation>()
            {
                {  0, new GoLocation()                                               },
                {  1, new RentableLocation(  1,  2,  60, PropertyGroup.Purple       )},
                {  2, new Location(          2,          PropertyGroup.Chest        )}, 
                {  3, new RentableLocation(  3,  4,  60, PropertyGroup.Purple       )},
                {  4, new IncomeTaxLocation()                                        }, 
                {  5, new RentableLocation(  5,  0,   0, PropertyGroup.Railroad     )}, 
                {  6, new RentableLocation(  6,  6, 100, PropertyGroup.LightGreen   )},
                {  7, new Location(          7,          PropertyGroup.Chance       )},
                {  8, new RentableLocation(  8,  6, 100, PropertyGroup.LightGreen   )},
                {  9, new RentableLocation(  9,  8, 120, PropertyGroup.LightGreen   )},
                { 10, new Location(         10,          PropertyGroup.JailVisiting )},
                { 11, new RentableLocation( 11, 10, 140, PropertyGroup.Violet       )},
                { 12, new RentableLocation( 12,  0,   0, PropertyGroup.Utility      )}, 
                { 13, new RentableLocation( 13, 10, 140, PropertyGroup.Violet       )},
                { 14, new RentableLocation( 14, 12, 160, PropertyGroup.Violet       )},
                { 15, new RentableLocation( 15,  0,   0, PropertyGroup.Railroad     )}, 
                { 16, new RentableLocation( 16, 14, 180, PropertyGroup.Orange       )},
                { 17, new Location(         17,          PropertyGroup.Chest        )},
                { 18, new RentableLocation( 18, 14, 180, PropertyGroup.Orange       )},
                { 19, new RentableLocation( 19, 16, 200, PropertyGroup.Orange       )},
                { 20, new Location(         20,          PropertyGroup.FreeParking  )}, 
                { 21, new RentableLocation( 21, 18, 220, PropertyGroup.Red          )},
                { 22, new ChanceLocation(   22,          PropertyGroup.Chance       )}, 
                { 23, new RentableLocation( 23, 18, 220, PropertyGroup.Red          )},
                { 24, new RentableLocation( 24, 20, 240, PropertyGroup.Red          )},
                { 25, new RentableLocation( 25,  0,   0, PropertyGroup.Railroad     )}, 
                { 26, new RentableLocation( 26, 22, 260, PropertyGroup.Yellow       )},
                { 27, new RentableLocation( 27, 22, 260, PropertyGroup.Yellow       )},
                { 28, new RentableLocation( 28,  0,   0, PropertyGroup.Utility      )}, 
                { 29, new RentableLocation( 29, 22, 280, PropertyGroup.Yellow       )},
                { 30, new JailLocation(                                             )},
                { 31, new RentableLocation( 31, 26, 300, PropertyGroup.DarkGreen    )},
                { 32, new RentableLocation( 32, 26, 300, PropertyGroup.DarkGreen    )},
                { 33, new Location(         33,          PropertyGroup.Chest        )}, 
                { 34, new RentableLocation( 34, 28, 320, PropertyGroup.DarkGreen    )},
                { 35, new RentableLocation( 35,  0,   0, PropertyGroup.Railroad     )}, 
                { 36, new Location(         36,          PropertyGroup.Chance       )}, 
                { 37, new RentableLocation( 37, 35, 350, PropertyGroup.DarkBlue     )},
                { 38, new LuxuryTaxLocation()                                        }, 
                { 39, new RentableLocation( 39, 50, 400, PropertyGroup.DarkBlue     )},
            };
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
