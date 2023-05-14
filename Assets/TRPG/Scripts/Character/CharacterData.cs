using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDS.TRPG;

namespace GDS.TRPG
{
    [CreateAssetMenu]
    public class CharacterData : ScriptableObject
    {
        public string characterName = "Nameless One";
        public CharacterAttributes attributes;
        public CharacterAttributes levelUpRates;
        public Level level;

        public Stats stats;

        public int GetDefenseValue(DamageType incomeDamageType)
        {
            int defense = 0;

            switch (incomeDamageType)
            {
                case DamageType.Physical:
                    defense += attributes.defense;
                    break;
                case DamageType.Magical:
                    defense += attributes.resistance;
                    break;
            }
            return defense;
        }
        public int GetDamage(DamageType damageType)
        {
            int damage = 0;

            switch (damageType)
            {
                case DamageType.Physical:
                    damage += attributes.strength;
                    break;
                case DamageType.Magical:
                    damage += attributes.magic;
                    break;
            }

            return damage;
        }
        public void AddExperience(int exp)
        {
            level.AddExperience(exp);
            if (level.CheckLevelUp() == true)
            {
                LevelUp();
            }
        }
        private void LevelUp()
        {
            level.LevelUp();
            LevelUpAttributes();
        }
        private void LevelUpAttributes()
        {
            for (int i = 0; i < CharacterAttributes.AttributeCount; i++)
            {
                int rate = levelUpRates.Get((CharacterAttributeEnum)i);
                rate += UnityEngine.Random.Range(0, 100);
                rate /= 100;
                if (rate > 0)
                {
                    attributes.Sum((CharacterAttributeEnum)i, rate);
                }
            }
        }

        public int GetIntValue(CharacterStats characterStats)
        {
            return stats.GetIntValue(characterStats);
        }
        public float GetFloatValue(CharacterStats characterStats)
        {
            return stats.GetFloatValue(characterStats);
        }
    }
}