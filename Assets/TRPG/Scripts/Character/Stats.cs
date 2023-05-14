using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDS.TRPG;

namespace GDS.TRPG
{
    public enum CharacterStats
    {
        HP,
        AttackRange,
        Accuracy,
        Dodge,
        CritChance,
        CritDamageMultiplicator,
        MovermentPoints
    }


    [Serializable]
    public class Stats
    {
        public int hp = 100;
        public int attackRange = 1;
        public float accuracy = 0.75f;
        public float dodge = 0.1f;
        public float critChance = 0.1f;
        public float critDamageMultiplicator = 1.5f;
        public float movermentPoints = 50f;

        public float GetFloatValue(CharacterStats stats)
        {
            switch (stats)
            {
                case CharacterStats.Accuracy:
                    return accuracy;
                case CharacterStats.Dodge:
                    return dodge;
                case CharacterStats.CritChance:
                    return critChance;
                case CharacterStats.CritDamageMultiplicator:
                    return critDamageMultiplicator;
                case CharacterStats.MovermentPoints:
                    return movermentPoints;
            }
            Debug.LogWarning("Requesting incorrect stats");
            return 0f;
        }
        public int GetIntValue(CharacterStats stats)
        {
            switch (stats)
            {
                case CharacterStats.HP:
                    return hp;
                case CharacterStats.AttackRange:
                    return attackRange;
            }
            Debug.LogWarning("Requesting incorrect stats");
            return 0;
        }
    }
}