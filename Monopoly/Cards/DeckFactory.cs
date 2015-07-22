using System;
using System.Collections.Generic;
using System.Linq;
using Monopoly.Tasks;

// List of cards came from:
// https://answers.yahoo.com/question/index?qid=20110528154141AAFwRyu


namespace Monopoly.Cards
{
    public class DeckFactory : IDeckFactory
    {
        Random random; 
        private ITaskHandler taskHandler;
       
        public DeckFactory(ITaskHandler taskHandler)
        {
            random = new Random();
            this.taskHandler = taskHandler;
        }

        public virtual IDeck BuildCommunitiyChestDeck()
        {
            var cards = new List<ICard>();

            cards.Add(new Card("Advance To Go",                    new MoveToLocationTask(      0, taskHandler), DeckType.Chest));
            cards.Add(new Card("Bank Error In Your Favor",         new CollectFromBankerTask(  75, taskHandler), DeckType.Chest));
            cards.Add(new Card("Doctor's Fees",                    new PayBankerTask(          50, taskHandler), DeckType.Chest));
            cards.Add(new Card("Get Out of Jail Card",             new GetOutOfJailTask(           taskHandler), DeckType.Chest));
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

            return new Deck(cards.OrderBy(x => random.Next()).ToList());
        }

        public virtual IDeck BuildChanceDeck()
        {
            var cards = new List<ICard>();

            cards.Add(new Card("Advance to Go (Collect $200)",         new MoveToLocationTask(      0, taskHandler), DeckType.Chance));
            cards.Add(new Card("Advance To Illinios Ave",              new MoveToLocationTask(     24, taskHandler), DeckType.Chance));
            cards.Add(new Card("Advance To Nearest Utility",           new MoveToNearestUtilityTask(   taskHandler), DeckType.Chance));
            cards.Add(new Card("Advance To Nearest Railroad",          new MoveToNearestRailroadTask(  taskHandler), DeckType.Chance));
            cards.Add(new Card("Advance To Nearest Railroad",          new MoveToNearestRailroadTask(  taskHandler), DeckType.Chance));
            cards.Add(new Card("Advance to St. Charles Place",         new MoveToLocationTask(      0, taskHandler), DeckType.Chance));
            cards.Add(new Card("Bank Pays you Dividend of $50",        new CollectFromBankerTask(  50, taskHandler), DeckType.Chance));
            cards.Add(new GetOutOfJailCard("Get Out of Jail Free",     new GetOutOfJailTask(           taskHandler), DeckType.Chance));
            cards.Add(new Card("Go Back 3 Spaces",                     new MoveDistanceTask(       -3, taskHandler), DeckType.Chance));
            cards.Add(new Card("Go Directly To Jail",                  new GoDirectlyToJailTask(       taskHandler), DeckType.Chance));
            // Make general repairs on your property, Not implemented in this release
            cards.Add(new Card("Pay Poor Tax of $15",                  new PayBankerTask(          15, taskHandler), DeckType.Chance));
            cards.Add(new Card("Take a trip to Reading Railroad",      new MoveToLocationTask(      5, taskHandler), DeckType.Chance)); 
            cards.Add(new Card("Take a Walk on the Boardwalk",         new MoveToLocationTask(     39, taskHandler), DeckType.Chance));
            cards.Add(new Card("Elected Chairman of the Board",        new PayAllOtherPlayersTask( 50, taskHandler), DeckType.Chance));
            cards.Add(new Card("Your Building Loan Matures",           new CollectFromBankerTask(  50, taskHandler), DeckType.Chance));
            cards.Add(new Card("You Have Won a Crossword Competition", new CollectFromBankerTask( 100, taskHandler), DeckType.Chance));

            return new Deck(cards.OrderBy(x => random.Next()).ToList());
        }
    }

    public enum DeckType
    {
        Chance,
        Chest
    }
}
