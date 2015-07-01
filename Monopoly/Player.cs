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

        public int GetOutOfJailCards = 0;

        public JailStrategy PreferedJailStrategy { get; set; }
             
        public Player(ILocation playerLocation, int startingBalance = DEFAULT_STARTING_BALANCE)
        {
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

        public bool HasGetOutOfJailCard()
        {
            return GetOutOfJailCards > 0;
        }

        public void UseGetOutOfJailCard()
        {
            GetOutOfJailCards--;
        }

        public void AddGetOutOfJailCard()
        {
            GetOutOfJailCards++;
        }
    }
}
