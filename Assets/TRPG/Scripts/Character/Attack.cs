using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDS.TRPG;

namespace GDS.TRPG
{
    public enum DamageType
    {
        Physical,
        Magical
    }

    public class Attack : MonoBehaviour
    {
        GridObject gridObject;
        CharacterAnimator characterAnimator;
        Character character;

        private void Awake()
        {
            gridObject = GetComponent<GridObject>();
            characterAnimator = GetComponentInChildren<CharacterAnimator>();
            character = GetComponent<Character>();
        }
        public void AttackGridObject(GridObject targetGridObject)
        {
            RotateCharacter(targetGridObject.transform.position);
            characterAnimator.Attack();

            //check accuracy for miss
            if (UnityEngine.Random.value >= character.GetFloatValue(CharacterStats.Accuracy)) { Debug.Log("Miss"); return; }

            Character target = targetGridObject.GetComponent<Character>();
            //check dodge
            if (UnityEngine.Random.value <= target.GetFloatValue(CharacterStats.Dodge)) { Debug.Log("Dodge"); return; }

            int damage = character.GetDamage();

            //check crit
            if (UnityEngine.Random.value <= character.GetFloatValue(CharacterStats.CritChance))
            {
                Debug.Log("Crit");
                damage = (int)(damage * character.GetFloatValue(CharacterStats.CritDamageMultiplicator));
            }

            //apply armor or magick resistance
            damage -= target.GetDefenseValue(character.damageType);

            if (damage <= 0)
            {
                damage = 1;
            }

            Debug.Log("target takes " + damage.ToString() + " damage");
            target.TakeDamage(damage);
        }

        private void RotateCharacter(Vector3 towards)
        {
            Vector3 direction = (towards - transform.position).normalized;
            direction.y = 0;
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}