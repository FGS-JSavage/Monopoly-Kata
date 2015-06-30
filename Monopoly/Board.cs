using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Locations;

namespace Monopoly
{
    public class Board
    {
        private Dice dice;
        private LocationManager locationManager;
        private Realtor realtor;
        private Banker banker;
        private Jailer jailer;

        public Board()
        {
            dice = new Dice();
            realtor = new Realtor();
            locationManager = new LocationManager(realtor);
            banker = new Banker();
            jailer = new Jailer();
        }

        public void DoTurn(IPlayer player)
        {
            dice.Roll();
            DoTurn(player, dice.Score, dice.WasDoubles);
        }

        public void DoTurn(IPlayer player, int distance, bool rolledDoubles)
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

        public void DoJailTurn(IPlayer player, int distance, bool rolledDoubles)
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

            player.PlayerLocation = locationManager.MovePlayer(player, distance);

            if (player.PlayerLocation.Group == PropertyGroup.Jail)
            {
                SendPlayerToJail(player);
            }

            player.CompleteLandOnLocationTasks();

            if (realtor.SpaceIsForSale(player.PlayerLocation.SpaceNumber))
            {
                realtor.MakePurchase(player, player.PlayerLocation.SpaceNumber);
            }
            else if (realtor.SpaceIsOwned(player.PlayerLocation.SpaceNumber)) // then it must be owned
            {
                realtor.ChargeRent(realtor.GetOwnerForSpace(player.PlayerLocation.SpaceNumber), player, distance);
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
                player.UseGetOutOfJailCard();
                jailer.ReleasePlayerFromJail(player);
                DoTurn(player);
            }
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

        public LocationManager GetLocationManager()
        {
            return locationManager;
        }

        public Realtor GetRealtor()
        {
            return realtor;
        }

        public Jailer GetJailer()
        {
            return jailer;
        }
    }
}
