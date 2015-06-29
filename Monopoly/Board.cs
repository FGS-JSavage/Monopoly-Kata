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
            }

            if (rolledDoubles)
            {
                player.DoublesCount++;
            }
            else
            {
                player.DoublesCount = 0;
            }

            if (player.DoublesCount == 3)
            {
                player.PlayerLocation = new JailLocation();
                player.DoublesCount = 0;
                return;
            }

            DoStandardTurn(player, distance, rolledDoubles);
        }

        public void DoJailTurn(IPlayer player, int distance, bool RolledDoubles)
        {
            if (jailer.GetRemainingSentence(player) == 0)
            {
                banker.ChargePlayerToGetOutOfJail(player);
                
            }

            switch (player.GetJailStrategy())
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

                default: // also handles "case JailStrategy.RollDoubles:"
                    HandleGetOutOfJailByRollingDoublesStrategy(player);
                    break;
            }
        }

        public void DoStandardTurn(IPlayer player, int distance, bool RolledDoubles)
        {
            player.CompleteExitLocationTasks();

            player.PlayerLocation = locationManager.MovePlayer(player, distance);

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

        public LocationManager GetLocationManager()
        {
            return locationManager;
        }

        public Realtor GetRealtor()
        {
            return realtor;
        }

        public void PayJailFine(IPlayer player)
        {
            player.Balance -= 50;
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

        public void HandleGetOutOfJailByRollingDoublesStrategy(IPlayer player)
        {
            dice.Roll();

            if (dice.WasDoubles) // Success
            {
                jailer.ReleasePlayerFromJail(player);
                DoTurn(player, dice.Score, false);
            }
            else // Failure
            {
                jailer.DecreaseSentence(player);

                if (jailer.GetRemainingSentence(player) == 0)
                {
                    HandleGetOutOfJailByPaying(player);
                    DoTurn(player, dice.Score, false);
                }
            }
        }

        public void HandleGetOutOfJailByPaying(IPlayer player)
        {
            banker.ChargePlayerToGetOutOfJail(player);
            jailer.ReleasePlayerFromJail(player);
        }
    }
}
