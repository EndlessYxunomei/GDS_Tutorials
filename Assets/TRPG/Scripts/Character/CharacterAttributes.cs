using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDS.TRPG;

namespace GDS.TRPG
{
    public enum CharacterAttributeEnum
    {
        Strength,
        Magic,
        Skill,
        Speed,
        Defense,
        Resistance
    }

    [Serializable]
    public class CharacterAttributes
    {
        public const int AttributeCount = 6;

        public int strength;
        public int magic;
        public int skill;
        public int speed;
        public int defense;
        public int resistance;

        public void Sum(CharacterAttributeEnum attribute, int value)
        {
            switch (attribute)
            {
                case CharacterAttributeEnum.Strength:
                    strength += value;
                    break;
                case CharacterAttributeEnum.Magic:
                    magic += value;
                    break;
                case CharacterAttributeEnum.Skill:
                    skill += value;
                    break;
                case CharacterAttributeEnum.Speed:
                    speed += value;
                    break;
                case CharacterAttributeEnum.Defense:
                    defense += value;
                    break;
                case CharacterAttributeEnum.Resistance:
                    resistance += value;
                    break;
            }
        }
        public int Get(CharacterAttributeEnum i)
        {
            switch (i)
            {
                case CharacterAttributeEnum.Strength:
                    return strength;
                case CharacterAttributeEnum.Magic:
                    return magic;
                case CharacterAttributeEnum.Skill:
                    return skill;
                case CharacterAttributeEnum.Speed:
                    return speed;
                case CharacterAttributeEnum.Defense:
                    return defense;
                case CharacterAttributeEnum.Resistance:
                    return resistance;
            }
            Debug.LogWarning("Trying to return Attribute value wich was not implemented");
            return -1;
        }

        public CharacterAttributes() { }
    }
}