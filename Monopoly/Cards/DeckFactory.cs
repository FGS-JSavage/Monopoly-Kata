using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Cards;
using Monopoly.Tasks;

namespace Monopoly
{
    public class DeckFactory
    {
        private Board board;
        private Banker banker;
        private List<IPlayer> players; 

        public DeckFactory(Board board, Banker banker, List<IPlayer> players)
        {
            this.board = board;
            this.banker = banker;
            this.players = players;
        }

        public Deck BuildCommuntiyChestDeck()
        {
            var deck = new List<ICard>();

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
            
            
            deck.Add(new Card("Advance To Go", new MoveToLocationTask(board)));
            deck.Add(new Card("Bank Error In Your Favor", new CollectFromBankerTask(75)));
            deck.Add(new Card("Doctor's Fees", new PayBankerTask(50)));
            deck.Add(new GetOutOfJailCard());
            deck.Add(new Card("Go Directly To Jail", new GoDirectlyToJailTask(board)));
            deck.Add(new Card("It Is Your Birthday", new CollectFromAllTask(10, players, banker)));
            deck.Add(new Card("Opera Night", new CollectFromAllTask(50, players, banker)));
            deck.Add(new Card("Income Tax Refund", new CollectFromBankerTask(20)));
            deck.Add(new Card("Life Insurance Matures", new CollectFromBankerTask(100)));
            deck.Add(new Card("Pay Hospital Fees", new PayBankerTask(100)));
            deck.Add(new Card("Pay Hospital Fees", new PayBankerTask(50)));
            deck.Add(new Card("Receive $25 Consultancy Fee", new CollectFromBankerTask(25)));
            // Street repairs, I don't have houses implemented though
            deck.Add(new Card("You have won second prize in a beauty contest", new CollectFromBankerTask(10)));
            deck.Add(new Card("You inherit $100", new CollectFromBankerTask(100)));
            deck.Add(new Card("From sale of stock you get $50", new CollectFromBankerTask(50)));
            deck.Add(new Card("Holiday Fund matures", new CollectFromBankerTask(100)));


            // TODO shuffle deck
 
            return new Deck(deck);
        }

        public Deck BuildChanceDeck()
        {
            var deck = new List<ICard>();

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
            // You have been elected chairman of the board – pay each player $50 
            // Your building loan matures – collect $150 
            // You have won a crossword competition - collect $100

            deck.Add(new Card("Advance to Go (Collect $200)", new MoveToLocationTask(board)));

            // TODO shuffle deck

            return new Deck(deck);
        }
    }
}
