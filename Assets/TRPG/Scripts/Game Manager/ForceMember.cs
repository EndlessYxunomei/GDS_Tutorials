using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDS.TRPG;

namespace GDS.TRPG
{
    public class ForceMember
    {
        public Character character;
        public CharacterTurn characterTurn;

        public ForceMember(Character character, CharacterTurn characterTurn)
        {
            this.character = character;
            this.characterTurn = characterTurn;
        }
    }
}