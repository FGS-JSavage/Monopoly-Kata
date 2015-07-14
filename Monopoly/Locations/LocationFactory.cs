using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;

namespace Monopoly.Locations
{
    public class LocationFactory : ILocationFactory
    {

        [Inject] public IDeckFactory deckFactory;

        public IDeck chanceDeck;
        public IDeck chestDeck;

        public LocationFactory(IDeckFactory deckFactory)
        {
            chanceDeck = deckFactory.BuildChanceDeck();
            chestDeck = deckFactory.BuildCommuntiyChestDeck();
             //Nothing
        }

        public Dictionary<int, ILocation> BuildLocations()
        {
            return new Dictionary<int, ILocation>()
            {
                {  0, new GoLocation()                                                  },
                {  1, new RentableLocation(  1,  2,  60,    PropertyGroup.Purple       )},
                {  2, new DrawCardLocation(  2, chestDeck,  PropertyGroup.Chest        )}, 
                {  3, new RentableLocation(  3,  4,  60,    PropertyGroup.Purple       )},
                {  4, new IncomeTaxLocation(                                           )}, 
                {  5, new RentableLocation(  5,  0,   0,    PropertyGroup.Railroad     )}, 
                {  6, new RentableLocation(  6,  6, 100,    PropertyGroup.LightGreen   )},
                {  7, new DrawCardLocation(  7, chanceDeck, PropertyGroup.Chance       )}, // Inject Deck
                {  8, new RentableLocation(  8,  6, 100,    PropertyGroup.LightGreen   )},
                {  9, new RentableLocation(  9,  8, 120,    PropertyGroup.LightGreen   )},
                { 10, new Location(         10,             PropertyGroup.JailVisiting )},
                { 11, new RentableLocation( 11, 10, 140,    PropertyGroup.Violet       )},
                { 12, new RentableLocation( 12,  0,   0,    PropertyGroup.Utility      )}, 
                { 13, new RentableLocation( 13, 10, 140,    PropertyGroup.Violet       )},
                { 14, new RentableLocation( 14, 12, 160,    PropertyGroup.Violet       )},
                { 15, new RentableLocation( 15,  0,   0,    PropertyGroup.Railroad     )}, 
                { 16, new RentableLocation( 16, 14, 180,    PropertyGroup.Orange       )},
                { 17, new DrawCardLocation( 17, chestDeck,  PropertyGroup.Chest         )}, // Inject Deck
                { 18, new RentableLocation( 18, 14, 180,    PropertyGroup.Orange       )},
                { 19, new RentableLocation( 19, 16, 200,    PropertyGroup.Orange       )},
                { 20, new Location(         20,             PropertyGroup.FreeParking  )}, 
                { 21, new RentableLocation( 21, 18, 220,    PropertyGroup.Red          )},
                { 22, new DrawCardLocation( 22, chanceDeck, PropertyGroup.Chance       )}, // Inject Deck
                { 23, new RentableLocation( 23, 18, 220,    PropertyGroup.Red          )},
                { 24, new RentableLocation( 24, 20, 240,    PropertyGroup.Red          )},
                { 25, new RentableLocation( 25,  0,   0,    PropertyGroup.Railroad     )}, 
                { 26, new RentableLocation( 26, 22, 260,    PropertyGroup.Yellow       )},
                { 27, new RentableLocation( 27, 22, 260,    PropertyGroup.Yellow       )},
                { 28, new RentableLocation( 28,  0,   0,    PropertyGroup.Utility      )}, 
                { 29, new RentableLocation( 29, 22, 280,    PropertyGroup.Yellow       )},
                { 30, new JailLocation(                                                )},
                { 31, new RentableLocation( 31, 26, 300,    PropertyGroup.DarkGreen    )},
                { 32, new RentableLocation( 32, 26, 300,    PropertyGroup.DarkGreen    )},
                { 33, new DrawCardLocation( 33, chestDeck,  PropertyGroup.Chest        )}, // Inject Deck
                { 34, new RentableLocation( 34, 28, 320,    PropertyGroup.DarkGreen    )},
                { 35, new RentableLocation( 35,  0,   0,    PropertyGroup.Railroad     )}, 
                { 36, new DrawCardLocation( 36, chanceDeck, PropertyGroup.Chance       )}, // Inject Deck
                { 37, new RentableLocation( 37, 35, 350,    PropertyGroup.DarkBlue     )},
                { 38, new LuxuryTaxLocation(                                           )}, 
                { 39, new RentableLocation( 39, 50, 400,    PropertyGroup.DarkBlue     )},
            };
        }

        //public ILocation GetClosest(int playerLocation, PropertyGroup desiredGroup)
        //{
        //    return locationKeeper.Values.Where(   x => x.Group == desiredGroup)
        //                                .OrderBy( y => Math.Abs((long)y.SpaceNumber - playerLocation))
        //                                .ThenBy(  z => z.SpaceNumber)
        //                                .First();
        //}

        public void Initialize()
        {

            if (deckFactory != null)
            {
                this.chanceDeck = deckFactory.BuildChanceDeck();
                this.chestDeck = deckFactory.BuildCommuntiyChestDeck();
            }
        }
    }
}
