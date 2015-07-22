using System;
using System.Security.Cryptography.X509Certificates;
using Monopoly.Cards;
using Monopoly.Handlers;
using Monopoly.Ninject;
using Ninject;
using NUnit.Framework;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace MonopolyUnitTests.HandlerTests
{
    [TestFixture]
    class CardHandlerUnitTests : IDisposable
    {
        private IKernel ninject;
        private IFixture fixture;
        private Mock<DeckFactory> mockDeckFactory;
        private Mock<Deck> mockChanceDeck;
        private Mock<Deck> mockChestDeck;
        private CardHandler cardHandler;

        [SetUp]
        public void Init()
        {
            fixture = new Fixture().Customize(new AutoMoqCustomization());
            ninject = new StandardKernel(new BindingsModule());

            mockChanceDeck = fixture.Create<Mock<Deck>>();
            mockChestDeck = fixture.Create<Mock<Deck>>();

            mockDeckFactory = fixture.Create<Mock<DeckFactory>>();
            mockDeckFactory.Setup(x => x.BuildChanceDeck()).Returns(mockChanceDeck.Object);
            mockDeckFactory.Setup(x => x.BuildCommunitiyChestDeck()).Returns(mockChestDeck.Object);

            ninject.Rebind<IDeckFactory>().ToConstant(mockDeckFactory.Object).InSingletonScope();

            cardHandler = ninject.Get<CardHandler>();
        }

        [TearDown]
        public void Dispose()
        {
            ninject.Dispose();
        }

        [Test]
        public void DrawChanceCard_CallsDrawOnDeckObject()
        {
            cardHandler.DrawChanceCard();

            mockChanceDeck.Verify(x => x.Draw());
        }

        [Test]
        public void DrawChestCard_CallsDrawOnRespectiveDeck()
        {
            cardHandler.DrawChestCard();

            mockChestDeck.Verify(x => x.Draw());
        }

        [Test]
        public void Discard_DiscardsChanceCardToCorrectDeck()
        {
            cardHandler.Discard(cardHandler.DrawChanceCard());

            mockChanceDeck.Verify(x => x.Draw());
        }

        [Test]
        public void Discard_DiscardsChestCardToCorrectDeck()
        {
            cardHandler.Discard(cardHandler.DrawChestCard());

            mockChestDeck.Verify(x => x.Draw());
        }
    }
}
