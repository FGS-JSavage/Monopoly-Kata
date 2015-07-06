﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Tasks
{
    public class PayBankerTask : IPlayerTask
    {
        private int amount;
        public PayBankerTask(int amount)
        {
            this.amount = amount;
        }

        public void Complete(IPlayer player)
        {
            player.Balance -= amount;
        }
    }
}