using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Tasks;
using Monopoly;
using Monopoly.Locations;
using Ninject;
using Ninject.Modules;

namespace Monopoly.Ninject
{
    public class BindingsModule : NinjectModule
    {
        public override void Load()
        {
            // NOT needed
            //Bind<ICard>().To<Card>();

            // Transient
            Bind<IPlayer>().To<Player>();
            Bind<ILocation>().To<GoLocation>(); // ?????

            // Fancy Singletons
            Bind<IDice>().To<Dice>();
            Bind<IBanker>().To<Banker>().InSingletonScope();
            Bind<IMovementHandler>().To<MovementHandler>();
            Bind<ITurnHandler>().To<TurnHandler>().InSingletonScope();
            Bind<PlayerFactory>().To<PlayerFactory>().WithConstructorArgument(6);
            Bind<ILocationFactory>().To<LocationFactory>();
            Bind<IDeckFactory>().To<DeckFactory>();
            Bind<IJailer>().To<Jailer>().InSingletonScope();
            Bind<ITaskHandler>().To<TaskHandler>().InSingletonScope();
            Bind<IRealtor>().To<Realtor>().InSingletonScope();
            Bind<ICardHandler>().To<CardHandler>().InSingletonScope();
            Bind<IGame>().To<Game>();
            Bind<IDeck>().To<Deck>();

            // After the fact
            

        }
    }
}
