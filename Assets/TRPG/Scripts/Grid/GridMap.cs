using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using GDS.TRPG;

namespace GDS.TRPG
{
    public class GridMap : MonoBehaviour
    {
        Node[,] grid;

        //x & y сетки
        public int length = 25;
        public int width = 25;
        [SerializeField] float cellSize = 1f;
        [SerializeField] LayerMask obstacleLayer;
        [SerializeField] LayerMask terrainLayer;

        private void Awake()
        {
            GenerateGrid();
        }

        private void GenerateGrid()
        {
            grid = new Node[length, width];

            for (int y = 0; y < width; y++)
            {
                for (int x = 0; x < length; x++)
                {
                    grid[x, y] = new Node();
                }
            }
            CalculateElevation();
            CheckPassableTerrain();
        }
        private void CalculateElevation()
        {
            for (int y = 0; y < width; y++)
            {
                for (int x = 0; x < length; x++)
                {
                    Ray ray = new Ray(GetWorldPosition(x, y) + Vector3.up * 100f, Vector3.down);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, float.MaxValue, terrainLayer))
                    {
                        grid[x, y].elevation = hit.point.y;
                    }
                }
            }
        }
        private void CheckPassableTerrain()
        {
            for (int y = 0; y < width; y++)
            {
                for (int x = 0; x < length; x++)
                {
                    Vector3 worldPosition = GetWorldPosition(x, y);
                    bool passable = !Physics.CheckBox(worldPosition, Vector3.one / 2 * cellSize, Quaternion.identity, obstacleLayer);
                    // grid[x, y] = new Node();
                    grid[x, y].passable = passable;
                }
            }
        }

        public bool CheckBoundary(Vector2Int positionOnGrid)
        {
            if (positionOnGrid.x < 0 || positionOnGrid.x >= length || positionOnGrid.y < 0 || positionOnGrid.y >= width)
            { return false; }
            return true;
        }
        public bool CheckBoundary(int posX, int posY)
        {
            if (posX < 0 || posX >= length || posY < 0 || posY >= width)
            { return false; }
            return true;
        }
        public bool CheckWalkable(int pos_x, int pos_y)
        {
            return grid[pos_x, pos_y].passable;
        }

        private void OnDrawGizmos()
        {
            if (grid == null)
            {
                for (int y = 0; y < width; y++)
                {
                    for (int x = 0; x < length; x++)
                    {
                        Vector3 pos = GetWorldPosition(x, y);
                        Gizmos.DrawCube(pos, Vector3.one / 4);
                    }
                }
            }
            else
            {
                for (int y = 0; y < width; y++)
                {
                    for (int x = 0; x < length; x++)
                    {
                        Vector3 pos = GetWorldPosition(x, y, true);
                        Gizmos.color = grid[x, y].passable ? Color.white : Color.red;
                        Gizmos.DrawCube(pos, Vector3.one / 4);
                    }
                }
            }

        }

        public Vector3 GetWorldPosition(int x, int y, bool elevation = false)
        {
            //поправка если сетка не в (0,0,0)
            //return new Vector3(transform.position.x + (x * cellSize), 0f, transform.position.z + (y * cellSize));

            return new Vector3(x * cellSize, elevation == true ? grid[x, y].elevation : 0f, y * cellSize);
        }
        public Vector2Int GetGridPossition(Vector3 worldPosition)
        {
            //поправка если сетка не в (0,0,0)
            //worldPosition -= transform.position;

            worldPosition.x += cellSize / 2;
            worldPosition.z += cellSize / 2;

            Vector2Int positionOnGrid = new Vector2Int((int)(worldPosition.x / cellSize), (int)(worldPosition.z / cellSize));
            return positionOnGrid;
        }

        public void PlaceObject(Vector2Int positionOnGrid, GridObject gridObject)
        {
            if (CheckBoundary(positionOnGrid))
            {
                grid[positionOnGrid.x, positionOnGrid.y].gridObject = gridObject;
            }
            else
            {
                Debug.Log("Object outside bounderies");
            }
        }
        public GridObject GetPlacedObject(Vector2Int gridPosition)
        {
            if (CheckBoundary(gridPosition))
            {
                GridObject gridObject = grid[gridPosition.x, gridPosition.y].gridObject;
                return gridObject;
            }
            return null;
        }

        public List<Vector3> ConvertPathNodesToWorldPositions(List<PathNode> path)
        {
            List<Vector3> worldPositions = new List<Vector3>();

            //ищем косяк
            // if (path == null) { return null; }
            for (int i = 0; i < path.Count; i++)
            {
                worldPositions.Add(GetWorldPosition(path[i].pos_x, path[i].pos_y, true));
            }

            return worldPositions;
        }

        public void RemoveObject(Vector2Int positionOnGrid, GridObject gridObject)
        {
            if (CheckBoundary(positionOnGrid))
            {
                //if (grid[positionOnGrid.x, positionOnGrid.y].gridObject != gridObject) { return; }
                grid[positionOnGrid.x, positionOnGrid.y].gridObject = null;
            }
            else
            {
                Debug.Log("Object outside bounderies");
            }
        }

        public bool CheckOccupied(Vector2Int positionOnGrid)
        {
            return GetPlacedObject(positionOnGrid) != null;
        }

        public bool CheckElevation(int from_pos_x, int from_pos_y, int to_pos_x, int to_pos_y, float characterClimb = 1.5f)
        {
            float from_elevation = grid[from_pos_x, from_pos_y].elevation;
            float to_elevation = grid[to_pos_x, to_pos_y].elevation;

            float elevaton_difference = to_elevation - from_elevation;
            elevaton_difference = Mathf.Abs(elevaton_difference);

            return characterClimb > elevaton_difference;
        }
    }
}