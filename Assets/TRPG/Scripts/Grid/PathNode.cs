using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDS.TRPG;

namespace GDS.TRPG
{
    public class PathNode
    {
        public int pos_x;
        public int pos_y;

        public float gValue;
        public float hValue;
        public PathNode parentNode;

        public float fValue
        {
            get { return gValue + hValue; }
        }

        public PathNode(int xPos, int yPos)
        {
            pos_x = xPos;
            pos_y = yPos;
        }

        public void Clear()
        {
            gValue = 0f;
            hValue = 0f;
            parentNode = null;
        }
    }
}