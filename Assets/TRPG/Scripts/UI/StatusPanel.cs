using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;
using GDS.TRPG;

namespace GDS.TRPG
{
    public class StatusPanel : MonoBehaviour
    {
        [SerializeField] TMPro.TextMeshProUGUI characterName;
        [SerializeField] Slider hpBar;
        [SerializeField] CharacterAttributeText strAttributeText;
        [SerializeField] CharacterAttributeText magAttributeText;
        [SerializeField] CharacterAttributeText sklAttributeText;
        [SerializeField] CharacterAttributeText spdAttributeText;
        [SerializeField] CharacterAttributeText defAttributeText;
        [SerializeField] CharacterAttributeText resAttributeText;
        [SerializeField] TMPro.TextMeshProUGUI levelText;
        [SerializeField] Slider expBar;

        /* [SerializeField] GameObject statusPanel;

         SelectCharacter selectCharacter;
         Character currentCharacterStatus;
         bool isActive;

         private void Awake()
         {
             selectCharacter = GetComponent<SelectCharacter>();
         }
         private void Update()
         {
             if (isActive == true)
             {
                 UpdateStatus(currentCharacterStatus);
                 if (selectCharacter.hoveringOverCharacter == null)
                 {
                     HideStatusPanel();
                     return;
                 }
                 if (selectCharacter.hoveringOverCharacter != currentCharacterStatus)
                 {
                     currentCharacterStatus = selectCharacter.hoveringOverCharacter;
                     UpdateStatus(currentCharacterStatus);
                     return;
                 }
             }
             else
             {
                 if (selectCharacter.hoveringOverCharacter != null)
                 {
                     currentCharacterStatus = selectCharacter.hoveringOverCharacter;
                     ShowStatusPanel();
                     return;
                 }
             }
         }

         private void HideStatusPanel()
         {
             statusPanel.SetActive(false);
             isActive = false;
         }
         private void ShowStatusPanel()
         {
             statusPanel.SetActive(true);
             isActive = true;
             UpdateStatus(currentCharacterStatus);
         }*/
        public void UpdateStatus(Character character)
        {
            hpBar.maxValue = character.hp.max;
            hpBar.value = character.hp.current;
            characterName.text = character.characterData.characterName;

            levelText.text = "LVL: " + character.characterData.level.level.ToString();
            expBar.maxValue = character.characterData.level.RequiredExperienceToLevelUp;
            expBar.value = character.characterData.level.experience;

            strAttributeText.UpdateText(character.characterData.attributes.Get(CharacterAttributeEnum.Strength));
            magAttributeText.UpdateText(character.characterData.attributes.Get(CharacterAttributeEnum.Magic));
            sklAttributeText.UpdateText(character.characterData.attributes.Get(CharacterAttributeEnum.Skill));
            spdAttributeText.UpdateText(character.characterData.attributes.Get(CharacterAttributeEnum.Speed));
            defAttributeText.UpdateText(character.characterData.attributes.Get(CharacterAttributeEnum.Defense));
            resAttributeText.UpdateText(character.characterData.attributes.Get(CharacterAttributeEnum.Resistance));
        }
    }
}