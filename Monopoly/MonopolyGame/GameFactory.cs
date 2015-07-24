using System;
using System.Collections.Generic;
using System.Linq;
using Monopoly.Handlers;
using Monopoly.Ninject;
using Monopoly.Player;
using Ninject;

namespace Monopoly.MonopolyGame
{
    public class GameFactory : IDisposable
    {
        IKernel ninject;

        public GameFactory()
        {
            ninject = new StandardKernel(new BindingsModule());
        }

        public Game BuildGame(int numberOfPlayers)
        {
            if (numberOfPlayers < 2 || numberOfPlayers > 8)
            {
                return null;
            }

            List<IPlayer> players = PlayerFactory.BuildPlayers(numberOfPlayers);

            return new Game(ninject.Get<TurnHandler>(), PlayerFactory.BuildPlayers(numberOfPlayers));
        }

        public Game BuildGame(params string[] names)
        {
            if (names.Length < 2 || names.Length > 8)
            {
                return null;
            }

            return new Game(ninject.Get<ITurnHandler>(), PlayerFactory.BuildPlayers(names.ToList()));
        }

        public void Dispose()
        {
            ninject.Dispose();
        }
    }
}
