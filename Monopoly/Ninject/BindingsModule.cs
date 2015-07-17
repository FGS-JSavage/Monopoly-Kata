using Monopoly.Board;
using Monopoly.Board.Locations;
using Monopoly.Cards;
using Monopoly.MonopolyGame;
using Monopoly.Handlers;
using Monopoly.Tasks;
using Ninject.Modules;

namespace Monopoly.Ninject
{
    public class BindingsModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPlayer>().To<Player>();
            Bind<ILocation>().To<GoLocation>();
            Bind<IMovementHandler>().To<MovementHandler>();
            Bind<IDice>().To<Dice>();
            Bind<ILocationFactory>().To<LocationFactory>();
            Bind<IDeckFactory>().To<DeckFactory>();
            Bind<IGame>().To<Game>();
            Bind<IDeck>().To<Deck>();

            Bind<IBanker>().To<Banker>().InSingletonScope();            
            Bind<ITurnHandler>().To<TurnHandler>().InSingletonScope();
            Bind<PlayerFactory>().To<PlayerFactory>().WithConstructorArgument(6);
            Bind<IJailer>().To<Jailer>().InSingletonScope();
            Bind<ITaskHandler>().To<TaskHandler>().InSingletonScope();
            Bind<IRealtor>().To<Realtor>().InSingletonScope();
            Bind<ICardHandler>().To<CardHandler>().InSingletonScope();
        }
    }
}
