using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Monopoly;
using Monopoly.Locations;

namespace MonopolyUnitTests
{
    [TestFixture]
    class GameUnitTests
    {
        private Game game;
        private Board board;
        private List<IPlayer> players;
        private LocationManager locationManager;
        private Realtor realtor;
        private Jailer jailer;
            
        [SetUp]
        public void Init()
        {

            game = new Game();
            board = game.GetBoard();
            players = game.GetPlayers();
            locationManager = board.GetLocationManager();
            realtor = board.GetRealtor();
            jailer = board.GetJailer();
        }

        // RELEASE 1 -----------------------------------------------------------------------

        [Test]
        public void Default_Game_Constructor_Initializes_Six_Players()
        {
            Assert.AreEqual(game.GetPlayers().Count, 6);
        }

        [Test]
        public void Default_Game_Constructor_Initializes_All_Players_To_Space_Zero()
        {
            foreach (var player in game.GetPlayers())
            {
                Assert.AreEqual(player.PlayerLocation.SpaceNumber, 0);    
            }
        }

        [Test]
        public void Move_Players_Twenty_Times()
        {
            foreach (var i in Enumerable.Range(0,20))
            {
                game.DoRound();
            }
        }

        // RELEASE 2 -----------------------------------------------------------------------

        [Test]
        public void Landing_On_Go_Balance_Increases_By_200()
        {
            Assert.AreEqual(0, players[0].Balance); // Confirm starting balance
            board.DoTurn(players[0], 40, false);

            Assert.AreEqual(200, players[0].Balance); // Confirm increase due to landing on Go
        }

        [Test]
        public void Landing_On_A_Normal_Location_Does_Not_Change_Balance()
        {
            Assert.AreEqual(0, players[0].Balance); // Confirm starting balance

            locationManager.MovePlayer(players[0], 35);

            Assert.AreEqual(0, players[0].Balance); // Confirm increase due to landing on Go
        }

        [Test]
        public void Passing_Go_Without_Landing_On_Go_Increases_Balance_By_200()
        {
            Assert.AreEqual(0, players[0].Balance); // Confirm starting balance

            board.DoTurn(players[0], 45, false);

            Assert.AreEqual(200, players[0].Balance); // Confirm increase due to passing Go
        }

        [Test]
        public void Passing_Go_Twice_In_One_Turn_Increases_Balance_By_400()
        {
            board.DoTurn(players[0], 85, false);

            Assert.AreEqual(400, players[0].Balance); // Confirm increase due to passing Go
        }

        [Test]
        public void Player_Start_Near_End_Roll_Enough_To_Pass_Go_Balance_Increases_By_200()
        {

            players[0].Balance = 400;

            board.DoTurn(players[0], 39, false);

            board.DoTurn(players[0], 1, false);

            Assert.AreEqual(200, players[0].Balance); // Confirm increase due to passing Go
        }

        [Test]
        [TestCase(1800, Result = 1620.0 )]
        [TestCase(2200, Result = 2000.0 )]
        [TestCase(0,    Result =    0.0 )]
        [TestCase(2000, Result = 1800.0 )]
        public double Land_On_Income_Tax_Charges_Correctly(int startingBalance)
        {
            // Income tax is space # 4

            players[0].Balance = startingBalance; // set initial balance

            board.DoTurn(players[0], 4, false); // move to income tax

            return players[0].Balance; 
        }

        [Test]
        public void Passing_Over_Income_Tax_Does_Not_Effect_Balance()
        {
            // Income tax is space # 4

            locationManager.MovePlayer(players[0], 6); // move to income tax

            Assert.AreEqual(players[0].Balance, 0); // Confirm no change in balance
        }

        [Test]
        [TestCase(1000, Result = 925.0 )]
        [TestCase(75,   Result =   0.0 )]
        [TestCase(50,   Result =   0.0 )]
        [TestCase(0,    Result =   0.0 )]
        public double Landing_On_Luxury_Tax_Decreases_Balance_By_75(int startingBalance)
        {
            // Income tax is space # 4

            players[0].Balance = startingBalance; // set initial balance

            board.DoTurn(players[0], 38, false); // move to income tax

            return players[0].Balance;
        }

        // Release 3 -------------------------------------------------------------------------------------------

        [Test]
        public void Player_Landing_On_Unowned_Property_Automagically_Buys_It()
        {
            // Space 1 is unowned

            players[0].Balance = 100; // set initial balance

            board.DoTurn(players[0], 1, false); // move to unowned property

            Assert.AreEqual(40, players[0].Balance);
        }

        [Test]
        public void Landing_On_Railroad_Charges_Correct_Rent_When_Only_One_Is_Owned()
        {
            var initalBalance = players[1].Balance;
            var expectedRent = 25;

            realtor.SetOwnerForSpace(players[0], 5);

            board.DoTurn(players[1], 5, false); // move to owned property

            Assert.AreEqual(expectedRent, Math.Abs(initalBalance - players[1].Balance));
        }


        [Test]
        public void Landing_On_Railroad_Charges_Correct_Rent_When_2_Are_Owned()
        {
            var initalBalance = players[1].Balance;
            var expectedRent = 50;

            realtor.SetOwnerForSpace(players[0], 5);
            realtor.SetOwnerForSpace(players[0], 15);

            board.DoTurn(players[1], 5, false); // move to owned property

            Assert.AreEqual(expectedRent, Math.Abs(initalBalance - players[1].Balance));
        }

        [Test]
        public void Landing_On_Railroad_Charges_Correct_Rent_When_3_Are_Owned()
        {
            var initalBalance = players[1].Balance;
            var expectedRent = 75;

            realtor.SetOwnerForSpace(players[0], 5);
            realtor.SetOwnerForSpace(players[0], 15);
            realtor.SetOwnerForSpace(players[0], 25);

            board.DoTurn(players[1], 5, false); // move to owned property

            Assert.AreEqual(expectedRent, Math.Abs(initalBalance - players[1].Balance));
        }

        [Test]
        public void Landing_On_Railroad_Charges_Correct_Rent_When_4_Are_Owned()
        {
            var initalBalance = players[1].Balance;
            var expectedRent = 100;

            realtor.SetOwnerForSpace(players[0], 5);
            realtor.SetOwnerForSpace(players[0], 15);
            realtor.SetOwnerForSpace(players[0], 25);
            realtor.SetOwnerForSpace(players[0], 35);

            board.DoTurn(players[1], 5, false); // move to owned property

            Assert.AreEqual(expectedRent, Math.Abs(initalBalance - players[1].Balance));
        }

        [Test]
        public void Landing_On_Utility_When_One_Is_Owned_Charges_4x_Roll()
        {
            var initalBalance = players[1].Balance;
            var expectedRent = 48;

            realtor.SetOwnerForSpace(players[0], 12);

            board.DoTurn(players[1], 12, false); // move to owned property

            Assert.AreEqual(expectedRent, Math.Abs(initalBalance - players[1].Balance));
        }

        [Test]
        public void Landing_On_Utility_When_Two_Are_Owned_By_Same_Player_Charges_10x_Roll()
        {
            var initalBalance = players[1].Balance;
            var expectedRent = 120;

            realtor.SetOwnerForSpace(players[0], 12);
            realtor.SetOwnerForSpace(players[0], 28);

            board.DoTurn(players[1], 12, false); // move to owned property

            Assert.AreEqual(expectedRent, Math.Abs(initalBalance - players[1].Balance));
        }

        [Test]
        public void Landing_On_Utility_When_Two_Are_Owned_By_Different_Players_Charges_10x_Roll()
        {
            var initalBalance = players[1].Balance;
            var expectedRent = 120;

            realtor.SetOwnerForSpace(players[0], 12);
            realtor.SetOwnerForSpace(players[3], 28);

            board.DoTurn(players[1], 12, false); // move to owned property

            Assert.AreEqual(expectedRent, Math.Abs(initalBalance - players[1].Balance));
        }

        [Test]
        public void Landing_On_Real_Estate_When_Owned_Charges_Single_Rent()
        {
            var initalBalance = players[1].Balance;
            var expectedRent = 6;

            realtor.SetOwnerForSpace(players[0], 6);

            board.DoTurn(players[1], 6, false); // move to owned property

            Assert.AreEqual(expectedRent, Math.Abs(initalBalance - players[1].Balance));
        }

        [Test]
        public void Landing_On_Real_Estate_When_2_Out_Of_Three_In_Group_Are_Owned_Charges_Single_Rent()
        {
            var initalBalance = players[1].Balance;
            var expectedRent = 6;

            realtor.SetOwnerForSpace(players[0], 6);
            realtor.SetOwnerForSpace(players[0], 8);

            board.DoTurn(players[1], 6, false); // move to owned property

            Assert.AreEqual(expectedRent, Math.Abs(initalBalance - players[1].Balance));
        }

        [Test]
        public void Landing_On_Real_Estate_When_All_In_Group_Are_Owned_Charges_Double_Rent()
        {
            var initalBalance = players[1].Balance;
            var expectedRent = 12;

            realtor.SetOwnerForSpace(players[0], 6);
            realtor.SetOwnerForSpace(players[0], 8);
            realtor.SetOwnerForSpace(players[0], 9);

            board.DoTurn(players[1], 6, false); // move to owned property

            Assert.AreEqual(expectedRent, Math.Abs(initalBalance - players[1].Balance));
        }

        [Test]
        public void RollNonDoublesAndLandOnGoToJail_PlayerIsInJail()
        {
            board.DoTurn(players[0], 30, false);

            Assert.True(jailer.PlayerIsImprisoned(players[0]));
        }

        [Test]
        public void RollDoublesAndLandOnGoToJail_PlayerIsInJail()
        {
            board.DoTurn(players[0], 30, true);

            Assert.That(players[0].PlayerLocation.Group, Is.EqualTo(PropertyGroup.Jail));
        }

        [Test]
        public void RollNonDoublesThreeTimesInARow_NeverPassOrLandOnGo_BalanceIsUnchangedAndPlayerIsInJail()
        {
            var initialBalance = players[0].Balance;
            board.DoTurn(players[0], 10, true);
            board.DoTurn(players[0], 0, true);
            board.DoTurn(players[0], 0, true);

            Assert.AreEqual(initialBalance, players[0].Balance);
            Assert.True(jailer.PlayerIsImprisoned(players[0]));
        }

        [Test]
        public void RollNonDoublesTwoTimesInARow_PlayerIsNotInJail()
        {
            board.DoTurn(players[0], 3, true);
            board.DoTurn(players[0], 3, true);
            
            Assert.False(jailer.PlayerIsImprisoned(players[0]));
        }

        [Test]
        public void PlayerPaysToGetOutOfJail_BalanceDecreasesBy50()
        {
            double intialBalance = players[0].Balance;
            jailer.Imprison(players[0]);
            board.HandleGetOutOfJailByPaying(players[0]);

            Assert.AreEqual(intialBalance - 50, players[0].Balance);
        }

        [Test]
        public void PlayerRollsToGetOutOfJail_RollsDoublesOnFirstTurn_MovesScoreOfRollAndStops()
        {
            players[0].PreferedJailStrategy = JailStrategy.RollDoubles;
            board.SendPlayerToJail(players[0]);

            board.DoTurn(players[0], 10, true);

            Assert.AreEqual(20, players[0].PlayerLocation.SpaceNumber);
        }

        [Test]
        public void PlayerRollsToGetOutOfJail_RollsDoublesOnSecondTurn_MovesScoreOfRollAndStops()
        {
            players[0].PreferedJailStrategy = JailStrategy.RollDoubles;
            board.SendPlayerToJail(players[0]);

            board.DoTurn(players[0], 10, false);
            board.DoTurn(players[0], 10, true);

            Assert.AreEqual(20, players[0].PlayerLocation.SpaceNumber);
        }

        [Test]
        public void PlayerRollsToGetOutOfJail_RollsDoublesOnThirdTurn_MovesScoreOfRollAndStops()
        {
            players[0].PreferedJailStrategy = JailStrategy.RollDoubles;
            board.SendPlayerToJail(players[0]);

            board.DoTurn(players[0], 10, false);
            board.DoTurn(players[0], 10, false);
            board.DoTurn(players[0], 10, true);

            Assert.AreEqual(20, players[0].PlayerLocation.SpaceNumber);
        }

        [Test]
        public void PlayerRollsToGetOutOfJail_TriesThreeTurnsWithoutSuccess_Pays50AndMovesDistanceRolledOnThirdTry()
        {
            players[0].PreferedJailStrategy = JailStrategy.RollDoubles;

            var initialBalance = players[0].Balance;

            board.SendPlayerToJail(players[0]);

            board.DoTurn(players[0], 10, false);
            board.DoTurn(players[0], 10, false);
            board.DoTurn(players[0], 10, false);

            Assert.AreEqual(20, players[0].PlayerLocation.SpaceNumber); // At Correct Location
            Assert.AreEqual(initialBalance - 50, players[0].Balance);
        }
    }
}
    