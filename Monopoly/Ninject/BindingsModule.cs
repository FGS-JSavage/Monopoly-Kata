using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Tasks;
using Monopoly;
using Monopoly.Locations;
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

            // Fancy Singletons
            Bind<IDice>().To<Dice>();
            Bind<IBanker>().To<Banker>().InSingletonScope();
            Bind<IMovementHandler>().To<MovementHandler>().InSingletonScope();
            Bind<ITurnHandler>().To<TurnHandler>().InSingletonScope();
            Bind<ILocationFactory>().To<LocationFactory>();
            Bind<IJailer>().To<Jailer>().InSingletonScope();
            Bind<ITaskHandler>().To<TaskHandler>().InSingletonScope();
            Bind<IRealtor>().To<Realtor>().InSingletonScope();
        }
    }
}
