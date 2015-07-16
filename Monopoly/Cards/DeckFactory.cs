using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Cards;
using Monopoly.Tasks;
using Ninject;
using Ninject.Infrastructure.Language;

namespace Monopoly
{
    public interface IDeckFactory
    {
        IDeck BuildCommunitiyChestDeck();
        IDeck BuildChanceDeck();
    }

    

    public class DeckFactory : IDeckFactory
    {
        Random random; 
        private ITaskHandler taskHandler;
       
        public DeckFactory(ITaskHandler taskHandler)
        {
            random = new Random();
            this.taskHandler = taskHandler;
        }

        public IDeck BuildCommunitiyChestDeck()
        {
            var cards = new List<ICard>();

            // --- Chest Cards ---
            // https://answers.yahoo.com/question/index?qid=20110528154141AAFwRyu
            //
            // Advance to Go (Collect $200) 
            // Bank error in your favor – collect $75 
            // Doctor's fees – Pay $50 
            // Get out of jail free – this card may be kept until needed, or sold 
            // Go to jail – go directly to jail – Do not pass Go, do not collect $200 
            // It is your birthday Collect $10 from each player 
            // Grand Opera Night – collect $50 from every player for opening night seats 
            // Income Tax refund – collect $20 
            // Life Insurance Matures – collect $100 
            // Pay Hospital Fees of $100 
            // Pay School Fees of $50 
            // Receive $25 Consultancy Fee 
            // You are assessed for street repairs – $40 per house, $115 per hotel 
            // You have won second prize in a beauty contest– collect $10 
            // You inherit $100 
            // From sale of stock you get $50 
            // Holiday Fund matures - Receive $100 
            
            cards.Add(new Card("Advance To Go",                    new MoveToLocationTask(      0, taskHandler), DeckType.Chest));
            cards.Add(new Card("Bank Error In Your Favor",         new CollectFromBankerTask(  75, taskHandler), DeckType.Chest));
            cards.Add(new Card("Doctor's Fees",                    new PayBankerTask(          50, taskHandler), DeckType.Chest));
            cards.Add(new Card("Get Out of Jail Card",             new GetOutOfJailTask(           taskHandler), DeckType.Chest)); // TODO Test
            cards.Add(new Card("Go Directly To Jail",              new GoDirectlyToJailTask(       taskHandler), DeckType.Chest));
            cards.Add(new Card("It Is Your Birthday",              new CollectFromAllTask(     10, taskHandler), DeckType.Chest));
            cards.Add(new Card("Opera Night",                      new CollectFromAllTask(     50, taskHandler), DeckType.Chest));
            cards.Add(new Card("Income Tax Refund",                new CollectFromBankerTask(  20, taskHandler), DeckType.Chest));
            cards.Add(new Card("Life Insurance Matures",           new CollectFromBankerTask( 100, taskHandler), DeckType.Chest));
            cards.Add(new Card("Pay Hospital Fees",                new PayBankerTask(         100, taskHandler), DeckType.Chest));
            cards.Add(new Card("Pay Hospital Fees",                new PayBankerTask(          50, taskHandler), DeckType.Chest));
            cards.Add(new Card("Receive $25 Consultancy Fee",      new CollectFromBankerTask(  25, taskHandler), DeckType.Chest));
            // Street repairs, I don't have houses implemented though
            cards.Add(new Card("You have won second prize in a beauty contest", new CollectFromBankerTask(10, taskHandler), DeckType.Chest));
            cards.Add(new Card("You inherit $100",                 new CollectFromBankerTask( 100, taskHandler), DeckType.Chest));
            cards.Add(new Card("From sale of stock you get $50",   new CollectFromBankerTask(  50, taskHandler), DeckType.Chest));
            cards.Add(new Card("Holiday Fund matures",             new CollectFromBankerTask( 100, taskHandler), DeckType.Chest));

            //return new Deck(cards);

            return new Deck(cards.OrderBy(x => random.Next()).ToList());
        }

        public IDeck BuildChanceDeck()
        {
            var cards = new List<ICard>();

            // Advance to Go (Collect $200) 
            // Advance to Illinois Ave. 
            // Advance token to nearest Utility. If unowned, you may buy it from the Bank. If owned, throw dice and pay owner a total ten times the amount thrown. 
            // Advance token to the nearest Railroad and pay owner twice the rental to which he/she is otherwise entitled. If Railroad is unowned, you may buy it from the Bank. (There are two of these.) 
            // Advance to St. Charles Place – if you pass Go, collect $200 
            // Bank pays you dividend of $50 
            // Get out of Jail free – this card may be kept until needed, or traded/sold 
            // Go back 3 spaces 
            // Go directly to Jail – do not pass Go, do not collect $200 
            // Make general repairs on all your property – for each house pay $25 – for each hotel $100 
            // Pay poor tax of $15 
            // Take a trip to Reading Railroad – if you pass Go collect $200 
            // Take a walk on the Boardwalk – advance token to Boardwalk 
            // You have been elected chairman of the TurnHandler – pay each player $50 
            // Your building loan matures – collect $150 
            // You have won a crossword competition - collect $100

            cards.Add(new Card("Advance to Go (Collect $200)",         new MoveToLocationTask(      0, taskHandler), DeckType.Chance));
            cards.Add(new Card("Advance To Illinios Ave",              new MoveToLocationTask(     24, taskHandler), DeckType.Chance));
            cards.Add(new Card("Advance To Nearest Utility",           new MoveToNearestUtilityTask(   taskHandler), DeckType.Chance));
            cards.Add(new Card("Advance To Nearest Railroad",          new MoveToNearestRailroadTask(  taskHandler), DeckType.Chance));
            cards.Add(new Card("Advance To Nearest Railroad",          new MoveToNearestRailroadTask(  taskHandler), DeckType.Chance)); // x2
            cards.Add(new Card("Advance to St. Charles Place",         new MoveToLocationTask(      0, taskHandler), DeckType.Chance));
            cards.Add(new Card("Bank Pays you Dividend of $50",        new CollectFromBankerTask(  50, taskHandler), DeckType.Chance));
            cards.Add(new GetOutOfJailCard("Get Out of Jail Free",     new GetOutOfJailTask(           taskHandler), DeckType.Chance));
            cards.Add(new Card("Go Back 3 Spaces",                     new MoveDistanceTask(       -3, taskHandler), DeckType.Chance));
            cards.Add(new Card("Go Directly To Jail",                  new GoDirectlyToJailTask(       taskHandler), DeckType.Chance));
            // Make general repairs on your property, Not implemented in this release
            cards.Add(new Card("Pay Poor Tax of $15",                  new PayBankerTask(          15, taskHandler), DeckType.Chance));
            cards.Add(new Card("Take a trip to Reading Railroad",      new MoveToLocationTask(      5, taskHandler), DeckType.Chance)); // collect 200 for go
            cards.Add(new Card("Take a Walk on the Boardwalk",         new MoveToLocationTask(     39, taskHandler), DeckType.Chance));
            cards.Add(new Card("Elected Chairman of the Board",        new PayAllOtherPlayersTask( 50, taskHandler), DeckType.Chance));
            cards.Add(new Card("Your Building Loan Matures",           new CollectFromBankerTask(  50, taskHandler), DeckType.Chance));
            cards.Add(new Card("You Have Won a Crossword Competition", new CollectFromBankerTask( 100, taskHandler), DeckType.Chance));

            //return new Deck(deck);

            return new Deck(cards.OrderBy(x => random.Next()).ToList());
        }
    }

    public enum DeckType
    {
        Chance,
        Chest
    }
}
