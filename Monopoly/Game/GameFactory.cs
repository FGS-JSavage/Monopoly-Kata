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
        public static void BuildGame(int numberOfPlayers)
        {
            List<IPlayer> players = PlayerFactory.BuildPlayers(numberOfPlayers);
            Banker banker = new Banker();
            Jailer jailer = new Jailer();

            //Lazy<Dictionary<int, ILocation>> properties = new Lazy<Dictionary<int, ILocation>>();

            Dictionary<int, ILocation> properties = LocationFactory.BuildLocations();

            Realtor realtor = new Realtor(banker, properties);
            MovementHandler movementHandler = new MovementHandler(realtor);
            TurnHandler turnHandler = new TurnHandler(realtor, jailer, banker, movementHandler);

            properties = LocationFactory.BuildLocations();

            Deck chestDeck = DeckFactory.BuildCommuntiyChestDeck(turnHandler, banker, players);
            Deck chanceDeck = DeckFactory.BuildChanceDeck(turnHandler, banker, players);

            /* -- Insert Decks into TurnHandler -- */

            TaskHandler taskHandler = new TaskHandler(movementHandler, players, banker, jailer);
        }
    }
}
