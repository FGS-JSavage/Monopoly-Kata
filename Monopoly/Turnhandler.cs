using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Locations;
using Ninject;

namespace Monopoly
{
    public class TurnHandler : ITurnHandler
    {
        //private IRealtor realtor;
        private IJailer  jailer;
        private IBanker  banker;
        private IMovementHandler movementHandler;
        private IDice dice;
        private IDeck chanceDeck;
        private IDeck chestDeck;

        //public TurnHandler(IRealtor realtor, IJailer jailer, IBanker banker, IMovementHandler movementHandler, IDice dice)
        public TurnHandler(IJailer jailer, IBanker banker, IMovementHandler movementHandler, IDice dice, IDeckFactory deckFactory)
        {
            //this.realtor = realtor;
            this.jailer = jailer;
            this.banker = banker;
            this.movementHandler = movementHandler;
            this.dice = dice;
            chestDeck = deckFactory.BuildCommunitiyChestDeck();
            chanceDeck = deckFactory.BuildChanceDeck();
        }

        public void DoTurn(IPlayer player)
        {
            dice.Roll();
            DoTurn(player, dice.Score, dice.WasDoubles);
        }

        public virtual void DoTurn(IPlayer player, int distance, bool rolledDoubles)
        {
            if (jailer.PlayerIsImprisoned(player))
            {
                DoJailTurn(player, distance, rolledDoubles);
                return;
            }

            // Doubles tracking logic
            if (rolledDoubles)
            {
                player.DoublesCount++;
            }
            else
            {
                player.DoublesCount = 0;
            }

            if (player.DoublesCount == 3) // Rolled 3 doubles. Send player directly to Jail
            {
                SendPlayerToJail(player);
                return; 
            }

            DoStandardTurn(player, distance, rolledDoubles);
        }

        public virtual void DoJailTurn(IPlayer player, int distance, bool rolledDoubles)
        {
            if (jailer.GetRemainingSentence(player) == 0) // Force player to pay for release
            {
                banker.ChargePlayerToGetOutOfJail(player);
                return;
            }

            switch (player.PreferedJailStrategy)
            {
                case JailStrategy.UseGetOutOfJailCard:
                    if (player.HasGetOutOfJailCard())
                        HandleGetOutOfJailUsingCardStrategy(player);
                    else
                        goto default;
                    break;
                
                case JailStrategy.Pay:
                    HandleGetOutOfJailByPaying(player);
                    break;

                default: // Handles "case JailStrategy.RollDoubles:"
                    HandleGetOutOfJailByRollingDoublesStrategy(player, distance, rolledDoubles);
                    break;
            }
        }

        public void DoStandardTurn(IPlayer player, int distance, bool RolledDoubles)
        {
            player.CompleteExitLocationTasks();

            //player.PlayerLocation = movementHandler.MovePlayer(player, distance);
            movementHandler.MovePlayer(player, distance);

            if (player.PlayerLocation.Group == PropertyGroup.Jail)
            {
                SendPlayerToJail(player);
            }

            player.CompleteLandOnLocationTasks();

            
            HandleDrawCardCase(player);
            
            movementHandler.HandleLanding(player, distance);

            //if (realtor.SpaceIsForSale(player.PlayerLocation.SpaceNumber))
            //{
            //    realtor.MakePurchase(player, player.PlayerLocation.SpaceNumber);
            //}
            //else if (realtor.SpaceIsOwned(player.PlayerLocation.SpaceNumber)) // then it must be owned
            //{
            //    realtor.ChargeRent(realtor.GetOwnerForSpace(player.PlayerLocation.SpaceNumber), player, distance);
            //}
        }

        private void HandleDrawCardCase(IPlayer player)
        {
            if (player.PlayerLocation.Group == PropertyGroup.Chance)
            {
                ICard card = chanceDeck.Draw();
                card.Tasks.ForEach(x => x.Complete(player));
                chanceDeck.Discard(card);
            }

            else if (player.PlayerLocation.Group == PropertyGroup.Chest)
            {

                ICard card = chestDeck.Draw();
                card.Tasks.ForEach(x => x.Complete(player));
                chestDeck.Discard(card);
            }
        }

        public void PayJailFine(IPlayer player)
        {
            banker.ChargePlayerToGetOutOfJail(player);
        }

        public void HandleGetOutOfJailUsingCardStrategy(IPlayer player)
        {
            if (player.HasGetOutOfJailCard())
            {
                player.DecrementGetOutOfJailCard();
                jailer.ReleasePlayerFromJail(player);
                DoTurn(player);
            }
        }

        public void MovePlayerDirectlyToSpace(IPlayer player, int spaceNumber)
        {
            player.CompleteExitLocationTasks();

            //player.PlayerLocation = movementHandler.MovePlayerDirectlyToSpaceNumber(player, spaceNumber);

            movementHandler.MovePlayerDirectlyToSpaceNumber(player, spaceNumber);

            player.CompleteLandOnLocationTasks();
        }

        public void MoveToClosest(IPlayer player, PropertyGroup destinationGroup)
        {
            movementHandler.MoveToClosest(player, destinationGroup);
        }

        public void HandleGetOutOfJailByRollingDoublesStrategy(IPlayer player, int distance, bool rolledDoubles)
        {
            if (rolledDoubles) // Success
            {
                ReleasePlayerFromJail(player);
                DoTurn(player, distance, false);
            }
            else // Failure
            {
                jailer.DecreaseSentence(player);

                if (jailer.GetRemainingSentence(player) == 0)
                {
                    HandleGetOutOfJailByPaying(player);
                    DoTurn(player, distance, rolledDoubles);
                }
            }
        }

        public void HandleGetOutOfJailByPaying(IPlayer player)
        {
            banker.ChargePlayerToGetOutOfJail(player);
            ReleasePlayerFromJail(player);
        }

        public void SendPlayerToJail(IPlayer player)
        {
            player.PlayerLocation = new JailLocation();
            jailer.Imprison(player);
            player.DoublesCount = 0;
        }

        public void ReleasePlayerFromJail(IPlayer player)
        {
            jailer.ReleasePlayerFromJail(player);
            player.PlayerLocation = new JailVisitingLocation();
        }

        public IMovementHandler GetLocationManager()
        {
            return movementHandler;
        }

        public IRealtor GetRealtor()
        {
            //return realtor;
            return null;
        }

        public IJailer GetJailer()
        {
            return jailer;
        }
    }
}
