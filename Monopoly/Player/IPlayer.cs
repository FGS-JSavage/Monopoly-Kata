﻿using Monopoly.Board.Locations;
using Monopoly.Cards;
using Monopoly.Player;

namespace Monopoly
{
    public interface IPlayer
    {
        ILocation PlayerLocation    { get; set; }
        double Balance              { get; set; }
        //int DoublesCount            { get; set; }
        string Name                 { get; set; }
        int RoundsPlayed            { get; set; }

        void CompleteLandOnLocationTasks();

        ICard SurrenderGetOutOfJailCard();
        bool HasGetOutOfJailCard();
        void AddGetOutOfJailCard(ICard card);
        void TrackDoublesRolled(bool rolledDoubles);
        bool DidRollDoublesThrice();

        JailStrategy PreferedJailStrategy { get; set; }
        JailStrategy GetJailStrategy();
    }
}
