using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDS.TRPG;

namespace GDS.TRPG
{
    public class SelectCharacter : MonoBehaviour
    {
        GridMap targetGrid;

        Vector2Int positionOnGrid = new Vector2Int(-1, -1);
        GridObject hoveringOverGridObject;
        bool isSelected;

        MouseInput mouseInput;
        CommandMenu commandMenu;
        GameMenu gameMenu;

        public Character hoveringOverCharacter;
        public Character selected;

        private void Awake()
        {
            mouseInput = GetComponent<MouseInput>();
            commandMenu = GetComponent<CommandMenu>();
            gameMenu = GetComponent<GameMenu>();
        }
        private void Start()
        {
            targetGrid = FindObjectOfType<StageManager>().stageGrid;
        }

        void Update()
        {
            if (positionOnGrid != mouseInput.positionOnGrid)
            {
                HoverOverObject();
            }

            SelectInput();
            DeselectInput();

        }

        private void HoverOverObject()
        {
            positionOnGrid = mouseInput.positionOnGrid;
            hoveringOverGridObject = targetGrid.GetPlacedObject(positionOnGrid);
            if (hoveringOverGridObject != null)
            {
                hoveringOverCharacter = hoveringOverGridObject.GetComponent<Character>();
            }
            else
            {
                hoveringOverCharacter = null;
            }
        }

        private void LateUpdate()
        {
            if (selected != null)
            {
                if (isSelected == false)
                {
                    selected = null;
                }
            }
        }
        private void SelectInput()
        {
            HoverOverObject();
            if (selected != null) { return; }
            if (gameMenu.panel.activeInHierarchy == true) { return; }
            if (Input.GetMouseButtonDown(0))
            {
                if (hoveringOverCharacter != null && selected == null)
                {
                    selected = hoveringOverCharacter;
                    isSelected = true;
                }
                UpdatePanel();
            }
        }
        private void DeselectInput()
        {
            if (Input.GetMouseButtonDown(1))
            {
                selected = null;
                UpdatePanel();
            }
        }
        private void UpdatePanel()
        {
            if (selected != null)
            {
                commandMenu.OpenPanel(selected.GetComponent<CharacterTurn>());
            }
            else
            {
                commandMenu.ClosePanel();
            }
        }

        public void Deselect()
        {
            isSelected = false;
            // selected = null;
        }
    }
}