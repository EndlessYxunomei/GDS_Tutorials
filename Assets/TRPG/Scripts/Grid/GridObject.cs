using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDS.TRPG;

namespace GDS.TRPG
{
    public class GridObject : MonoBehaviour
    {
        public GridMap targetGrid;

        public Vector2Int positionOnGrid;


        private void Start()
        {
            Init();
        }

        private void Init()
        {
            positionOnGrid = targetGrid.GetGridPossition(transform.position);
            targetGrid.PlaceObject(positionOnGrid, this);
            Vector3 pos = targetGrid.GetWorldPosition(positionOnGrid.x, positionOnGrid.y, true);
            transform.position = pos;
        }
    }
}