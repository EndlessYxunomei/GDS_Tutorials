using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Burst.CompilerServices;
using UnityEngine;
using GDS.TRPG;

namespace GDS.TRPG
{
    public class MoveCharacter : MonoBehaviour
    {
        GridMap targetGrid;
        GridHighlight gridHighlight;
        Pathfinding pathfinding;

        private void Start()
        {
            StageManager stageManager = FindObjectOfType<StageManager>();
            targetGrid = stageManager.stageGrid;
            gridHighlight = stageManager.moveHighlight;
            pathfinding = targetGrid.GetComponent<Pathfinding>();
            //CheckWalkableTerrain();
        }

        /*private void Update()
        {
            /*if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, float.MaxValue, terrainLayerMask))
                {
                    Vector2Int gridPosition = targetGrid.GetGridPossition(hit.point);

                    //path = pathfinding.FindPath(targetCharacter.positionOnGrid.x, targetCharacter.positionOnGrid.y, gridPosition.x, gridPosition.y);
                    path = pathfinding.TraceBackPath(gridPosition.x, gridPosition.y);
                    path.Reverse();

                    if (path == null || path.Count == 0) { return; }

                    targetCharacter.GetComponent<Movement>().Move(path);
                }
            }
        }*/
        public List<PathNode> GetPath(Vector2Int from)
        {
            List<PathNode> path = pathfinding.TraceBackPath(from.x, from.y);
            if (path == null || path.Count == 0) { return null; }
            path.Reverse();

            return path;
        }
        public void CheckWalkableTerrain(Character targetCharacter)
        {
            GridObject gridObject = targetCharacter.GetComponent<GridObject>();
            List<PathNode> walkableNodes = new List<PathNode>();
            pathfinding.Clear();
            pathfinding.CalculateWalkableNodes(gridObject.positionOnGrid.x, gridObject.positionOnGrid.y, targetCharacter.GetFloatValue(CharacterStats.MovermentPoints), ref walkableNodes);
            gridHighlight.Hide();
            gridHighlight.Highlight(walkableNodes);
        }

        public bool CheckOccupied(Vector2Int positionOnGrid)
        {
            return targetGrid.CheckOccupied(positionOnGrid);
        }
    }
}