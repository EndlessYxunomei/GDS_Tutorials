using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDS.TRPG;

namespace GDS.TRPG
{
    public class LevelUpTestManager : MonoBehaviour
    {
        public Character targetCharacter;

        public void AddExperience(int exp)
        {
            targetCharacter.AddExperience(exp);
        }
    }
}
