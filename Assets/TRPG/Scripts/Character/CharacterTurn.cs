using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDS.TRPG;

namespace GDS.TRPG
{
    public enum Allegiance
    {
        Player,
        Opponent
    }

    public class CharacterTurn : MonoBehaviour
    {
        public bool canWalk;
        public bool canAct;
        public Allegiance allegiance;

        private void Start()
        {
            AddToRoundManager();
            GrantTurn();
        }

        private void AddToRoundManager()
        {
            RoundManager.instance.AddMe(this);
        }

        public void GrantTurn()
        {
            canWalk = true;
            canAct = true;
        }
    }
}