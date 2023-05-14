using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDS.TRPG;

namespace GDS.TRPG
{
    public class StatusPanelManager : MonoBehaviour
    {
        [SerializeField] GameObject statusPanelGameObject;
        [SerializeField] StatusPanel statusPanel;
        SelectCharacter selectCharacter;
        [SerializeField] Character currentCharacterStatus;
        bool isActive;
        [SerializeField] bool fixedCharacter;
        private void Awake()
        {
            selectCharacter = GetComponent<SelectCharacter>();
        }
        private void Update()
        {
            if (fixedCharacter == true)
            {
                statusPanel.UpdateStatus(currentCharacterStatus);
            }
            else
            {
                MouseHoverOverObject();
            }
        }

        private void MouseHoverOverObject()
        {
            if (isActive == true)
            {
                statusPanel.UpdateStatus(currentCharacterStatus);
                if (selectCharacter.hoveringOverCharacter == null)
                {
                    HideStatusPanel();
                    return;
                }
                if (selectCharacter.hoveringOverCharacter != currentCharacterStatus)
                {
                    currentCharacterStatus = selectCharacter.hoveringOverCharacter;
                    statusPanel.UpdateStatus(currentCharacterStatus);
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
            statusPanelGameObject.SetActive(false);
            isActive = false;
        }
        private void ShowStatusPanel()
        {
            statusPanelGameObject.SetActive(true);
            isActive = true;
            statusPanel.UpdateStatus(currentCharacterStatus);
        }

    }
}