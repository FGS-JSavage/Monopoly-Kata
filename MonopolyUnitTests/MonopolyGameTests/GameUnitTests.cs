using Monopoly;
using Monopoly.MonopolyGame;
using NUnit.Framework;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace MonopolyUnitTests.MonopolyGameTests
{
    [TestFixture]
    class GameUnitTests
    {
        private const int PLAYER_COUNT = 3;
        private Game game;
        private IFixture fixture;
        private Mock<ITurnHandler> mockTurnHandler;

        [SetUp]
        public void Init()
        {
            fixture = new Fixture().Customize(new AutoMoqCustomization());

            mockTurnHandler = fixture.Create<Mock<ITurnHandler>>();
            game = new Game(mockTurnHandler.Object, PlayerFactory.BuildPlayers(PLAYER_COUNT)); 
        }

        [Test]
        public void DoTurn_CallsTurnHandlerDoTurn()
        {
            game.DoTurn(It.IsAny<IPlayer>());

            mockTurnHandler.Verify(x => x.DoTurn(It.IsAny<IPlayer>()), Times.Once);
        }

        [Test]
        public void DoRound_CallsDoTurnForEachPlayer()
        {
            game.DoRound();

            mockTurnHandler.Verify(x => x.DoTurn(It.IsAny<IPlayer>()), Times.Exactly(PLAYER_COUNT));
        }
    }
}
