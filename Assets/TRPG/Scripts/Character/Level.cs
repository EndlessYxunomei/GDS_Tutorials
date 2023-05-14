using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDS.TRPG;

namespace GDS.TRPG
{
    [Serializable]
    public class Level
    {
        public int level = 1;
        public int experience = 0;

        public int RequiredExperienceToLevelUp
        {
            get
            {
                return level * 1000;
            }
        }

        public void AddExperience(int exp)
        {
            experience += exp;
        }
        public bool CheckLevelUp()
        {
            return experience >= RequiredExperienceToLevelUp;
        }
        public void LevelUp()
        {
            experience -= RequiredExperienceToLevelUp;
            level++;
        }
    }
}