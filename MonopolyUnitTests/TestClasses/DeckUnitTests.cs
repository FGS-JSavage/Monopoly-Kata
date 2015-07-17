using System.Collections.Generic;
using Monopoly.Cards;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace MonopolyUnitTests.TestClasses
{
    [TestFixture]
    class DeckUnitTests
    {
        private Mock<Card> mockCard1;
        private Mock<Card> mockCard2;
        private Mock<Card> mockCard3;
        private Deck deck;

        [SetUp]
        public void Init()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            mockCard1 = fixture.Create<Mock<Card>>();
            mockCard2 = fixture.Create<Mock<Card>>();
            mockCard3 = fixture.Create<Mock<Card>>();

            List<ICard> cards = new List<ICard>()
            {
                mockCard1.Object, 
                mockCard2.Object, 
                mockCard3.Object
            };

            deck = new Deck(cards);           
        }

        [Test]
        public void DrawCardDrawsFromFrontOfQueue_DiscardInsertsCardToBackOfQueue()
        {
            var card = deck.Draw();

            Assert.AreEqual(mockCard1.Object, card);

            deck.Discard(card);

            card = deck.Draw();

            Assert.AreEqual(mockCard2.Object, card);

            deck.Discard(card);

            card = deck.Draw();

            Assert.AreEqual(mockCard3.Object, card);

            deck.Discard(card);

            card = deck.Draw();

            Assert.AreEqual(mockCard1.Object, card); // Looped

            deck.Discard(card);
        }
    }
}
