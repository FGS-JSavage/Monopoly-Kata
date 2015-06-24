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
            
        [SetUp]
        public void Init()
        {
            game = new Game();
            board = game.GetBoard();
            players = game.GetPlayers();
            locationManager = board.GetLocationManager();
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
            board.DoTurn(players[0], 40);

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

            board.DoTurn(players[0], 45);

            Assert.AreEqual(200, players[0].Balance); // Confirm increase due to passing Go
        }

        [Test]
        public void Passing_Go_Twice_In_One_Turn_Increases_Balance_By_400()
        {
            board.DoTurn(players[0], 85);


            Assert.AreEqual(400, players[0].Balance); // Confirm increase due to passing Go
        }

        [Test]
        public void Player_Start_Near_End_Roll_Enough_To_Pass_Go_Balance_Increases_By_200()
        {
            board.DoTurn(players[0], 39);

            board.DoTurn(players[0]);

            Assert.AreEqual(200, players[0].Balance); // Confirm increase due to passing Go
        }

        [Test]
        public void Landing_On_Jail_Defaults_To_Just_Visiting()
        {
            // Jail is space # 30
            board.DoTurn(players[0], 30);

            Assert.That(players[0].PlayerLocation, Is.TypeOf(typeof(JailVisitingLocation))); // Confirm increase due to passing Go
        }

        [Test]
        public void Passing_Over_Jail_Without_Passing_Go_Does_Not_Change_Balance()
        {
            // Jail is space # 30

            locationManager.MovePlayer(players[0], 29); // Move to space before Jail

            board.DoTurn(players[0]); // Move by rolling Roll


            Assert.AreEqual(players[0].Balance, 0); // Confirm no change in balance
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

            board.DoTurn(players[0], 4); // move to income tax

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

            board.DoTurn(players[0], 38); // move to income tax

            return players[0].Balance;
        }
    }
}
    