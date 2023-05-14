using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDS.TRPG;

namespace GDS.TRPG
{
    public class CommandManager : MonoBehaviour
    {
        Command currentCommand;
        VictoryConditionManager victoryConditionManager;
        CommandInput commandInput;
        ClearUtility clearUtility;
        private void Awake()
        {
            commandInput = GetComponent<CommandInput>();
            clearUtility = GetComponent<ClearUtility>();
            victoryConditionManager = GetComponent<VictoryConditionManager>();
        }

        private void Start()
        {
            //commandInput = GetComponent<CommandInput>();
        }
        private void Update()
        {
            if (currentCommand != null)
            {
                ExecuteCommand();
            }
        }

        public void ExecuteCommand()
        {
            switch (currentCommand.commandType)
            {
                case CommandType.MoveTo:
                    {
                        MovementCommandExecute();
                        break;
                    }
                case CommandType.Attack:
                    {
                        AttackCommandExecute();
                        break;
                    }
            }
        }

        private void AttackCommandExecute()
        {
            Character receiver = currentCommand.character;
            receiver.GetComponent<Attack>().AttackGridObject(currentCommand.target);
            receiver.GetComponent<CharacterTurn>().canAct = false;
            //пока проверка сдесь, потом перенесем
            victoryConditionManager.CheckPlayerVictory();
            currentCommand = null;
            clearUtility.ClearGridHighlightAttack();
        }

        private void MovementCommandExecute()
        {
            Character receiver = currentCommand.character;
            receiver.GetComponent<Movement>().Move(currentCommand.path);
            receiver.GetComponent<CharacterTurn>().canWalk = false;
            currentCommand = null;
            clearUtility.ClearPathfinding();
            clearUtility.ClearGridHighlightMove();
        }

        public void AddMoveCommand(Character character, Vector2Int selectedGrid, List<PathNode> path)
        {
            currentCommand = new Command(character, selectedGrid, CommandType.MoveTo);
            currentCommand.path = path;
        }

        public void AddAttackCommand(Character attacker, Vector2Int selectedGrid, GridObject target)
        {
            currentCommand = new Command(attacker, selectedGrid, CommandType.Attack);
            currentCommand.target = target;
        }
    }
}