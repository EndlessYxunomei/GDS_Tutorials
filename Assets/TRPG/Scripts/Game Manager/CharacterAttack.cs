using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using GDS.TRPG;

namespace GDS.TRPG
{
    public class CharacterAttack : MonoBehaviour
    {
        // [SerializeField] GridObject selectedCharacter;
        GridMap targetGrid;
        GridHighlight highlight;
        //  [SerializeField] LayerMask terrainLayerMask;

        List<Vector2Int> attackPositions;

        private void Start()
        {
            StageManager stageManager = FindObjectOfType<StageManager>();
            targetGrid = stageManager.stageGrid;
            highlight = stageManager.attackHighlight;
        }
        private void Update()
        {
            /*if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, float.MaxValue, terrainLayerMask))
                {
                    Vector2Int gridPosition = targetGrid.GetGridPossition(hit.point);

                    if (attackPositions.Contains(gridPosition)) 
                    {
                        GridObject gridObject = targetGrid.GetPlacedObject(gridPosition);
                        if (gridObject == null) { return; }
                        selectedCharacter.GetComponent<Attack>().AttackPossition(gridObject);
                    }
                }

            }*/
        }

        public void CalculateAttackArea(Vector2Int characterPositionOnGrid, int attackRange, bool selfTargetable = false)
        {
            if (attackPositions == null)
            {
                attackPositions = new List<Vector2Int>();
            }
            else
            {
                attackPositions.Clear();
            }

            for (int x = -attackRange; x <= attackRange; x++)
            {
                for (int y = -attackRange; y <= attackRange; y++)
                {
                    if (selfTargetable == false)
                    {
                        if (x == 0 && y == 0) { continue; }
                    }

                    if (Mathf.Abs(x) + Mathf.Abs(y) > attackRange) { continue; }
                    if (targetGrid.CheckBoundary(characterPositionOnGrid.x + x, characterPositionOnGrid.y + y) == true)
                    {
                        attackPositions.Add(new Vector2Int(characterPositionOnGrid.x + x, characterPositionOnGrid.y + y));
                    }
                }
            }
            highlight.Hide();
            highlight.Highlight(attackPositions);
        }

        public bool Check(Vector2Int positionOnGrid)
        {
            return attackPositions.Contains(positionOnGrid);
        }

        public GridObject GetAttackTarget(Vector2Int positionOnGrid)
        {
            GridObject target = targetGrid.GetPlacedObject(positionOnGrid);
            return target;
        }
    }
}