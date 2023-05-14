using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDS.TRPG;

namespace GDS.TRPG
{
    public enum CommandType
    {
        MoveTo,
        Attack
    }

    public class Command
    {
        public Character character;
        public Vector2Int selectedGrid;
        public CommandType commandType;
        public List<PathNode> path;
        public GridObject target;

        public Command(Character character, Vector2Int selectedGrid, CommandType commandType)
        {
            this.character = character;
            this.selectedGrid = selectedGrid;
            this.commandType = commandType;
        }
    }
}