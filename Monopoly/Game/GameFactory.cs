using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity.Configuration;
using Monopoly.Locations;
using Monopoly.Ninject;
using Monopoly.Tasks;
using Ninject;

namespace Monopoly
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

            return new Game(ninject.Get<TurnHandler>(), PlayerFactory.BuildPlayers(names));
        }
    }
}
