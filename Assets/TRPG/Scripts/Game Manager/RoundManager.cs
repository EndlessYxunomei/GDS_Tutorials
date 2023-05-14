using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDS.TRPG;

namespace GDS.TRPG
{
    public class RoundManager : MonoBehaviour
    {
        [SerializeField] TMPro.TextMeshProUGUI turnCountText;
        [SerializeField] TMPro.TextMeshProUGUI forceRoundText;
        [SerializeField] ForceContainer playerForceContainer;
        [SerializeField] ForceContainer opponentForceContainer;

        int round = 1;
        Allegiance currentTurn;

        [SerializeField] MouseInput mouseInput;

        public static RoundManager instance;

        private void Awake()
        {
            instance = this;
        }
        private void Start()
        {
            UpdateTextOnScreen();
        }
        public void AddMe(CharacterTurn character)
        {
            if (character.allegiance == Allegiance.Opponent)
            {
                opponentForceContainer.AddMe(character);
            }
            if (character.allegiance == Allegiance.Player)
            {
                playerForceContainer.AddMe(character);
            }
        }
        public void NextRound()
        {
            round++;


            /*foreach (CharacterTurn character in characters)
            {
                character.GrantTurn();
            }*/
        }
        public void NextTurn()
        {
            switch (currentTurn)
            {
                case Allegiance.Player:
                    {
                        DisablePlayerInput();
                        currentTurn = Allegiance.Opponent;
                        break;
                    }
                case Allegiance.Opponent:
                    {
                        NextRound();
                        EnablePlayerInput();
                        currentTurn = Allegiance.Player;
                        break;
                    }
            }
            GrantTurnToForce();
            UpdateTextOnScreen();
        }

        private void EnablePlayerInput()
        {
            mouseInput.enabled = true;
        }

        private void DisablePlayerInput()
        {
            mouseInput.enabled = false;
        }

        private void GrantTurnToForce()
        {
            switch (currentTurn)
            {
                case Allegiance.Player:
                    {
                        playerForceContainer.GrantTurn();
                        break;
                    }
                case Allegiance.Opponent:
                    {
                        opponentForceContainer.GrantTurn();
                        break;
                    }
            }
        }
        private void UpdateTextOnScreen()
        {
            turnCountText.text = "Turn: " + round.ToString();
            forceRoundText.text = currentTurn.ToString();
        }
    }
}