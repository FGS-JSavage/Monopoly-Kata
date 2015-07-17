using System.Collections.Generic;
using Monopoly.Handlers;
using Monopoly.Ninject;
using Ninject;

namespace Monopoly.MonopolyGame
{
    public class GameFactory
    {
        public static Game BuildGame(int numberOfPlayers)
        {
            if (numberOfPlayers < 2 || numberOfPlayers > 8)
            {
                return null;
            }

            List<IPlayer> players = PlayerFactory.BuildPlayers(numberOfPlayers);


            IKernel ninject = new StandardKernel(new BindingsModule());

            return new Game(ninject.Get<TurnHandler>(), PlayerFactory.BuildPlayers(numberOfPlayers));
        }

        public static Game BuildGame(List<string> names)
        {
            if (names.Count < 2 || names.Count > 8)
            {
                return null;
            }

            IKernel ninject = new StandardKernel(new BindingsModule());

            return new Game(ninject.Get<ITurnHandler>(), PlayerFactory.BuildPlayers(names));
        }
    }
}
