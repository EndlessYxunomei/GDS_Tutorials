using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDS.TRPG;

namespace GDS.TRPG
{
    public class ForceContainer : MonoBehaviour
    {
        public Allegiance allegiance;
        public List<ForceMember> force;

        public void AddMe(CharacterTurn characterTurn)
        {
            if (force == null)
            {
                force = new List<ForceMember>();
            }
            force.Add(new ForceMember(characterTurn.GetComponent<Character>(), characterTurn));
            characterTurn.transform.parent = transform;
        }

        public void GrantTurn()
        {
            for (int i = 0; i < force.Count; i++)
            {
                force[i].characterTurn.GrantTurn();
            }
        }
        public bool CheckDefeated()
        {
            for (int i = 0; i < force.Count; i++)
            {
                if (force[i].character.defeated == false)
                {
                    return false;
                }
            }
            return true;
        }
    }
}