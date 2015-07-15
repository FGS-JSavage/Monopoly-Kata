using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Cards;
using Monopoly.Locations;
using Ninject;

namespace Monopoly
{
    public class TurnHandler : ITurnHandler
    {
        private IJailer  jailer;
        private IBanker  banker;
        private IMovementHandler movementHandler;
        private IDice dice;
        private ICardHandler cardHandler;

        public TurnHandler(IJailer jailer, IBanker banker, IMovementHandler movementHandler, IDice dice, ICardHandler cardHandler)
        {
            this.jailer = jailer;
            this.banker = banker;
            this.movementHandler = movementHandler;
            this.dice = dice;
            this.cardHandler = cardHandler;
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

            switch (player.GetJailStrategy())
            {
                case JailStrategy.UseGetOutOfJailCard:
                    HandleGetOutOfJailUsingCardStrategy(player);
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

            movementHandler.MovePlayer(player, distance);

            if (player.PlayerLocation.Group == PropertyGroup.Jail)
            {
                SendPlayerToJail(player);
            }

            player.CompleteLandOnLocationTasks();

            
            HandleDrawCardCase(player);
            
            movementHandler.HandleLanding(player, distance);
        }

        private void HandleDrawCardCase(IPlayer player)
        {
            if (player.PlayerLocation.Group == PropertyGroup.Chance)
            {
                ICard card = cardHandler.DrawChanceCard();

                if (card.GetType() == typeof (GetOutOfJailCard))
                {
                    player.AddGetOutOfJailCard(card);
                }
                
                CompleteCardTasks(player, card);
                Discard(card);
            }

            else if (player.PlayerLocation.Group == PropertyGroup.Chest)
            {
                ICard card = DrawChestCard();
                card.Tasks.ForEach(x => x.Complete(player));
                Discard(card);
            }
        }

        public void PayJailFine(IPlayer player)
        {
            banker.ChargePlayerToGetOutOfJail(player);
        }

        public void HandleGetOutOfJailUsingCardStrategy(IPlayer player)
        {
            cardHandler.Discard(player.SurrenderGetOutOfJailCard());
           jailer.ReleasePlayerFromJail(player);
           DoTurn(player);
        }

        public void MovePlayerDirectlyToSpace(IPlayer player, int spaceNumber)
        {
            player.CompleteExitLocationTasks();

            movementHandler.MovePlayerDirectlyToSpaceNumber(player, spaceNumber);

            player.CompleteLandOnLocationTasks();
        }

        public void MoveToClosest(IPlayer player, PropertyGroup destinationGroup)
        {
            movementHandler.MoveToClosest(player, destinationGroup);
        }

        public void HandleGetOutOfJailByRollingDoublesStrategy(IPlayer player, int distance, bool rolledDoubles)
        {
            if (rolledDoubles) 
            {
                ReleasePlayerFromJail(player);
                DoTurn(player, distance, false);
            }
            else 
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

        public void ReleasePlayerFromJailUsingCard(IPlayer player)
        {
            
        }


        public void ReleasePlayerFromJail(IPlayer player)
        {
            jailer.ReleasePlayerFromJail(player);
            player.PlayerLocation = new JailVisitingLocation();
        }

        public ICard DrawChanceCard()
        {
            return cardHandler.DrawChanceCard();
        }

        public ICard DrawChestCard()
        {
            return cardHandler.DrawChestCard();
        }

        public void Discard(ICard card)
        {
            cardHandler.Discard(card);
        }

        public void CompleteCardTasks(IPlayer player, ICard card)
        {
            card.Tasks.ForEach(x => x.Complete(player));
        }
    }
}
