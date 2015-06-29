﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public interface IGame
    {
        void DoRound();
        void DoTurn(IPlayer player);
        List<IPlayer> GetPlayers();
    }
}