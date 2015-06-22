﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public interface IPlayer
    {
        void MoveDistance(int distance);
        ILocation GetLocation();

        ILocation Location { get; set; }
        int Balance        { get; set; }
    }
}