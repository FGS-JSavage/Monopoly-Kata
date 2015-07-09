using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Handlers;
using Monopoly.Locations;
using Monopoly.Tasks;

namespace Monopoly
{
    public class GameFactory
    {

        /*
         * 
         * Maybe use delegates for discard.  
         * 
         * 
         * 

        */

        public static void BuildGame(int numberOfPlayers)
        {
            List<IPlayer> players = PlayerFactory.BuildPlayers(numberOfPlayers);
            Banker banker = new Banker();
            Jailer jailer = new Jailer();

            Realtor realtor = new Realtor(banker);
            MovementHandler movementHandler = new MovementHandler(realtor);
            
            TaskHandler taskHandler = new TaskHandler(movementHandler, players, banker, jailer);

            Deck chestDeck = DeckFactory.BuildCommuntiyChestDeck(taskHandler);
            Deck chanceDeck = DeckFactory.BuildChanceDeck(taskHandler);

            realtor.AddProperties(LocationFactory.BuildLocations(chestDeck, chanceDeck));

            TurnHandler turnHandler = new TurnHandler(realtor, jailer, banker, movementHandler);

            //return new Game(turnHandler); // TODO
        }
    }
}
