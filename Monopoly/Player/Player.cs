using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Locations;

namespace Monopoly
{
    public class Player : IPlayer
    {
        private const int DEFAULT_STARTING_BALANCE = 0;
       
        public virtual ILocation PlayerLocation { get; set; }
        public double Balance   { get; set; }
        public int DoublesCount { get; set; }
        public string Name      { get; set; }
        public int RoundsPlayed { get; set; }

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

        public void CompleteExitLocationTasks()
        {
            PlayerLocation.GetOnExitTasks().ForEach(x => x.Complete(this));
        }

        public void AddGetOutOfJailCard(ICard card)
        {
            getOutOfJailFreeCards.Push(card);
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
            switch(PreferedJailStrategy)
            {
                case JailStrategy.UseGetOutOfJailCard:
                    if (HasGetOutOfJailCard())
                        return PreferedJailStrategy;
                    goto default;

                case JailStrategy.Pay:
                    return PreferedJailStrategy;

                default: // Handles "case JailStrategy.RollDoubles:"
                    return JailStrategy.RollDoubles;
            }
        }
    }

    public enum JailStrategy
    {
        RollDoubles,
        Pay,
        UseGetOutOfJailCard
    }
}
