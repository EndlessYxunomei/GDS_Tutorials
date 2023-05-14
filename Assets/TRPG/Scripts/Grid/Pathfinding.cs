using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using GDS.TRPG;

namespace GDS.TRPG
{
    [RequireComponent(typeof(GridMap))]
    public class Pathfinding : MonoBehaviour
    {
        GridMap gridMap;

        PathNode[,] pathNodes;

        void Awake()
        {
            Init();
        }

        private void Init()
        {
            if (gridMap == null) { gridMap = GetComponent<GridMap>(); }
            pathNodes = new PathNode[gridMap.length, gridMap.width];

            for (int x = 0; x < gridMap.length; x++)
            {
                for (int y = 0; y < gridMap.width; y++)
                {
                    pathNodes[x, y] = new PathNode(x, y);
                }
            }
        }

        public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
        {
            PathNode startNode = pathNodes[startX, startY];
            PathNode endNode = pathNodes[endX, endY];
            List<PathNode> openList = new List<PathNode>();
            List<PathNode> closeList = new List<PathNode>();

            openList.Add(startNode);

            while (openList.Count > 0)
            {
                PathNode currentNode = openList[0];

                //расчет параметров клетки пути
                for (int i = 0; i < openList.Count; i++)
                {
                    if (currentNode.fValue > openList[i].fValue)
                    {
                        currentNode = openList[i];
                    }
                    if (currentNode.fValue == openList[i].fValue && currentNode.hValue > openList[i].hValue)
                    {
                        currentNode = openList[i];
                    }
                }

                openList.Remove(currentNode);
                closeList.Add(currentNode);

                if (currentNode == endNode)
                {
                    return RetracePath(startNode, endNode);
                }

                //Нахождение соседних клеток
                List<PathNode> neighbourNodes = new List<PathNode>();
                for (int x = -1; x < 2; x++)
                {
                    for (int y = -1; y < 2; y++)
                    {
                        if (x == 0 && y == 0) { continue; }
                        if (gridMap.CheckBoundary(currentNode.pos_x + x, currentNode.pos_y + y) == false) { continue; }
                        neighbourNodes.Add(pathNodes[currentNode.pos_x + x, currentNode.pos_y + y]);
                    }
                }
                //Расчет параметров соседних клеток
                for (int i = 0; i < neighbourNodes.Count; i++)
                {
                    float newMovementCostToNeighbour = currentNode.gValue + CalculateDistance(currentNode, neighbourNodes[i]);
                    if (closeList.Contains(neighbourNodes[i]) && newMovementCostToNeighbour >= neighbourNodes[i].gValue) { continue; }
                    if (gridMap.CheckWalkable(neighbourNodes[i].pos_x, neighbourNodes[i].pos_y) == false) { continue; }

                    float movementCost = currentNode.gValue + CalculateDistance(currentNode, neighbourNodes[i]);

                    if (openList.Contains(neighbourNodes[i]) == false || movementCost < neighbourNodes[i].gValue)
                    {
                        neighbourNodes[i].gValue = movementCost;
                        neighbourNodes[i].hValue = CalculateDistance(neighbourNodes[i], endNode);
                        neighbourNodes[i].parentNode = currentNode;

                        if (openList.Contains(neighbourNodes[i]) == false)
                        {
                            openList.Add(neighbourNodes[i]);
                        }
                    }
                }
            }
            return null;
        }

        private int CalculateDistance(PathNode currentNode, PathNode target)
        {
            int distX = Mathf.Abs(currentNode.pos_x - target.pos_x);
            int distY = Mathf.Abs(currentNode.pos_y - target.pos_y);

            if (distX > distY) { return 14 * distY + 10 * (distX - distY); }
            return 14 * distX + 10 * (distY - distX);
        }

        private List<PathNode> RetracePath(PathNode startNode, PathNode endNode)
        {
            List<PathNode> path = new List<PathNode>();
            PathNode currentNode = endNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.parentNode;
            }
            path.Reverse();
            return path;
        }

        public void CalculateWalkableNodes(int startX, int startY, float range, ref List<PathNode> toHighlight)
        {
            PathNode startNode = pathNodes[startX, startY];
            List<PathNode> openList = new List<PathNode>();
            List<PathNode> closeList = new List<PathNode>();

            openList.Add(startNode);

            while (openList.Count > 0)
            {
                PathNode currentNode = openList[0];

                openList.Remove(currentNode);
                closeList.Add(currentNode);

                List<PathNode> neighbourNodes = new List<PathNode>();
                for (int x = -1; x < 2; x++)
                {
                    for (int y = -1; y < 2; y++)
                    {
                        if (x == 0 && y == 0) { continue; }
                        if (gridMap.CheckBoundary(currentNode.pos_x + x, currentNode.pos_y + y) == false) { continue; }
                        neighbourNodes.Add(pathNodes[currentNode.pos_x + x, currentNode.pos_y + y]);
                    }
                }
                for (int i = 0; i < neighbourNodes.Count; i++)
                {
                    float newMovementCostToNeighbour = currentNode.gValue + CalculateDistance(currentNode, neighbourNodes[i]);
                    if (closeList.Contains(neighbourNodes[i]) && newMovementCostToNeighbour >= neighbourNodes[i].gValue) { continue; }
                    if (gridMap.CheckWalkable(neighbourNodes[i].pos_x, neighbourNodes[i].pos_y) == false) { continue; }
                    if (gridMap.CheckElevation(currentNode.pos_x, currentNode.pos_y, neighbourNodes[i].pos_x, neighbourNodes[i].pos_y) == false) { continue; }

                    float movementCost = currentNode.gValue + CalculateDistance(currentNode, neighbourNodes[i]);

                    if (movementCost > range) { continue; }

                    if (openList.Contains(neighbourNodes[i]) == false || movementCost < neighbourNodes[i].gValue)
                    {
                        neighbourNodes[i].gValue = movementCost;
                        neighbourNodes[i].parentNode = currentNode;

                        if (openList.Contains(neighbourNodes[i]) == false)
                        {
                            openList.Add(neighbourNodes[i]);
                        }
                    }
                }
            }
            toHighlight.AddRange(closeList);
        }

        public List<PathNode> TraceBackPath(int x, int y)
        {
            //сделал дополнительную проверку на возможность пойти в клетку, а не только на проверку на границы сетки
            if (gridMap.CheckBoundary(x, y) == false || gridMap.CheckWalkable(x, y) == false) { return null; }

            List<PathNode> path = new List<PathNode>();

            PathNode currentNode = pathNodes[x, y];
            while (currentNode.parentNode != null)
            {
                path.Add(currentNode);
                currentNode = currentNode.parentNode;
            }

            return path;
        }

        public void Clear()
        {
            for (int x = 0; x < gridMap.length; x++)
            {
                for (int y = 0; y < gridMap.width; y++)
                {
                    pathNodes[x, y].Clear();
                }
            }
        }
    }
}