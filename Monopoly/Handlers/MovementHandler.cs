﻿using Monopoly.Board;
using Monopoly.Board.Locations;
using Monopoly.Player;

namespace Monopoly.Handlers
{
    public class MovementHandler : IMovementHandler
    {
        private IRealtor realtor;
        private const int NUMBER_OF_SPACES = 40;
        private const int PASSING_GO_REWARD = 200;

        public MovementHandler(IRealtor realtor)
        {
            this.realtor = realtor;
        }

        public void MovePlayer(IPlayer player, int distance)
        {
            int nextSpaceNumber = player.PlayerLocation.SpaceNumber + distance;

            while (nextSpaceNumber > NUMBER_OF_SPACES) // Handles Flying over go
            {
                player.Balance += PASSING_GO_REWARD;

                nextSpaceNumber -= NUMBER_OF_SPACES;
            }

            MovePlayerToLocation(player, realtor.LocationForSpaceNumber(ChompToBoardSize(nextSpaceNumber)));

            HandleNormalLanding(player, distance);
        }

        public void MovePlayerDirectlyToSpaceNumber(IPlayer player, int spaceNumber)
        {
            ILocation nextLocation = realtor.LocationForSpaceNumber(spaceNumber);

            MovePlayerToLocation(player, nextLocation);
        }

        public void MoveToNearestRailroad(IPlayer player)
        {
            ILocation closestRailroad = realtor.GetClosest(player.PlayerLocation.SpaceNumber, PropertyGroup.Railroad);

            MovePlayerToLocation(player, closestRailroad);

            if (realtor.SpaceIsForSale(player.PlayerLocation.SpaceNumber))
            {
                HandlePurchasing(player);
            }
            else
            {
                realtor.ChargeDoubleRailroadRent(player);
            }
        }
        
        public void MoveToNearestUtility(IPlayer player, IDice dice)
        {
            ILocation closestUtility = realtor.GetClosest(player.PlayerLocation.SpaceNumber, PropertyGroup.Utility);

            MovePlayerToLocation(player, closestUtility);

            if (realtor.SpaceIsForSale(player.PlayerLocation.SpaceNumber))
            {
                HandlePurchasing(player);
            }
            else
            {
                dice.Roll();
                realtor.ChargeTenTimesRollValueRent(player, dice.Score);
            }
        }

        public void MovePlayerToLocation(IPlayer player, ILocation location)
        {
            player.PlayerLocation = location;
            player.CompleteLandOnLocationTasks();
        }

        public void HandlePurchasing(IPlayer player)
        {
            if (realtor.SpaceIsForSale(player.PlayerLocation.SpaceNumber))
            {
                realtor.MakePurchase(player);
            }
        }

        public void HandleNormalLanding(IPlayer player, int distance)
        {
            if (realtor.SpaceIsForSale(player.PlayerLocation.SpaceNumber))
            {
                realtor.MakePurchase(player);
            }
            else if (realtor.SpaceIsOwned(player.PlayerLocation.SpaceNumber))
            {
                realtor.ChargeRent(player, distance);
            }            
        }

        public int ChompToBoardSize(int spaceNumber)
        {
            return (spaceNumber + NUMBER_OF_SPACES) % NUMBER_OF_SPACES;
        }
    }
}