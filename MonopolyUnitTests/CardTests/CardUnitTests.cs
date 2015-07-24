using Monopoly.Cards;
using Monopoly.Tasks;
using NUnit.Framework;

namespace MonopolyUnitTests.CardTests
{
    [TestFixture]
    class CardUnitTests
    {
        private Card card;
        private IPlayerTask someTask;
        private DeckType someType;
        private string someName;

        [Test]
        public void CardConstructorCorrectlyStoresName()
        {
            card = new Card(someName, someTask, someType);

            Assert.AreSame(someName, card.Name);
        }

        [Test]
        public void CardConstructorCorrectlyStoresTask()
        {
            card = new Card(someName, someTask, someType);

            Assert.AreSame(someTask, card.Tasks[0]);
        }

        [Test]
        public void CardConstructorCorrectlyStoresCardType()
        {
            someType = DeckType.Chance;

            card = new Card(someName, someTask, someType);

            Assert.AreEqual(someType, card.Type);
        }
    }
}
