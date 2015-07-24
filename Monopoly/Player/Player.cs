using System.Collections.Generic;
using Monopoly.Board.Locations;
using Monopoly.Cards;

namespace Monopoly.Player
{
    public class Player : IPlayer
    {
        private const int DEFAULT_STARTING_BALANCE = 0;
       
        public ILocation PlayerLocation { get; set; }
        public double Balance   { get; set; }
        public string Name      { get; set; }
        public int RoundsPlayed { get; set; }
        private int DoublesCount { get; set; }

        private Stack<ICard> getOutOfJailFreeCards;
        public JailStrategy PreferedJailStrategy { get; set; }
             
        public Player(ILocation playerLocation, int startingBalance = DEFAULT_STARTING_BALANCE)
        { 
            getOutOfJailFreeCards = new Stack<ICard>();

            PlayerLocation = playerLocation;
            Balance = startingBalance;
        }
        
        public void CompleteLandOnLocationTasks()
        {
            PlayerLocation.GetOnLandTasks().ForEach(x => x.Complete(this));
        }

        public void AddGetOutOfJailCard(ICard card)
        {
            getOutOfJailFreeCards.Push(card);
        }

        public void TrackDoublesRolled(bool rolledDoubles)
        {
            DoublesCount = rolledDoubles ? ++DoublesCount : 0;
        }

        public bool DidRollDoublesThrice()
        {
            return DoublesCount >= 3;
        }

        public ICard SurrenderGetOutOfJailCard()
        {
            return getOutOfJailFreeCards.Pop();
        }

        public bool HasGetOutOfJailCard()
        {
            return getOutOfJailFreeCards.Count > 0;
        }

        public JailStrategy GetJailStrategy()
        {
            return !HasGetOutOfJailCard() && PreferedJailStrategy == JailStrategy.UseGetOutOfJailCard ? JailStrategy.RollDoubles : PreferedJailStrategy;
        }
    }

    public enum JailStrategy
    {
        RollDoubles,
        Pay,
        UseGetOutOfJailCard
    }
}
