using System;
using Monopoly.Cards;
using Monopoly.Ninject;
using Ninject;
using NUnit.Framework;

namespace MonopolyUnitTests.CardTests
{
    [TestFixture]
    class DeckFactoryUnitTests : IDisposable
    {
        private IKernel ninject;
        private DeckFactory deckFactory;
        private IDeck chanceDeck;
        private IDeck chestDeck ;

        [SetUp]
        public void Init()
        {
            ninject = new StandardKernel(new BindingsModule());

            deckFactory = ninject.Get<DeckFactory>();
            chanceDeck = deckFactory.BuildChanceDeck();
            chestDeck = deckFactory.BuildCommunitiyChestDeck();
        }

        [TearDown]
        public void Dispose()
        {
            ninject.Dispose();
        }

        [Test]
        public void DeckFactory_BuildsChanceDeckOfCorrectSize()
        {
            int expectedChanceCardCount = 16;
            int cardCount = 0;

            while (chanceDeck.Draw() != null)
            {
                cardCount++;
            }

            Assert.AreEqual(expectedChanceCardCount, cardCount);
        }

        [Test]
        public void DeckFactory_BuildsChestDeckOfCorrectSize()
        {
            int expectedChestCardCount = 16;
            int cardCount = 0;

            while (chestDeck.Draw() != null)
            {
                cardCount++;
            }

            Assert.AreEqual(expectedChestCardCount, cardCount);
        }
    }
}
