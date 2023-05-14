using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDS.TRPG;

namespace GDS.TRPG
{
    [Serializable]
    public class Int2Val
    {
        public int current;
        public int max;
        public bool canGoNegative;

        public Int2Val(int current, int max)
        {
            this.current = current;
            this.max = max;
        }

        internal void Subtract(int amount)
        {
            current -= amount;
            if (canGoNegative == false && current < 0)
            {
                current = 0;
            }
        }
    }
}
