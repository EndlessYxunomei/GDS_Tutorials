using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDS.TRPG;

namespace GDS.TRPG
{
    //данный класс исключительно тля тестирования грида - изменить, когда исчезнет необходимость
    public class Character : MonoBehaviour
    {
        public CharacterData characterData;
        public Int2Val hp = new Int2Val(100, 100);
        public DamageType damageType = DamageType.Physical;

        public bool defeated;

        CharacterAnimator characterAnimator;

        /*private void Start()
        {
            if (attributes == null)
            {
                Init();
            }
        }*/

        public int GetDefenseValue(DamageType incomeDamageType)
        {
            int defense = 0;


            return defense;
        }
        public int GetDamage()
        {
            int damage = 0;



            return damage;
        }
        /*public void Init()
        {
            attributes = new CharacterAttributes();
            level = new Level();
        }*/
        public void TakeDamage(int damage)
        {
            hp.Subtract(damage);
            CheckDefeat();
        }
        public void AddExperience(int exp)
        {
            characterData.AddExperience(exp);
        }
        private void CheckDefeat()
        {
            if (hp.current <= 0)
            {
                Defeated();
            }
            else
            {
                Flinch();
            }
        }
        private void Flinch()
        {
            if (characterAnimator == null)
            {
                characterAnimator = GetComponentInChildren<CharacterAnimator>();
            }
            characterAnimator.Flinch();
        }
        private void Defeated()
        {
            if (characterAnimator == null)
            {
                characterAnimator = GetComponentInChildren<CharacterAnimator>();
            }
            defeated = true;
            characterAnimator.Defeated();
        }
        public int GetIntValue(CharacterStats characterStats)
        {
            return characterData.GetIntValue(characterStats);
        }
        public float GetFloatValue(CharacterStats characterStats)
        {
            return characterData.GetFloatValue(characterStats);
        }
        public void SetCharacterData(CharacterData characterData)
        {
            this.characterData = characterData;
        }
    }
}