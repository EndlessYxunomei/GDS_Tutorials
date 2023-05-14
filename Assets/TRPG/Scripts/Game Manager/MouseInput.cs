using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDS.TRPG;

namespace GDS.TRPG
{
    public class MouseInput : MonoBehaviour
    {
        [SerializeField] Camera mainCamera;
        GridMap targetGrid;
        [SerializeField] LayerMask terrainLayerMask;


        public Vector2Int positionOnGrid;
        public bool active;

        private void Start()
        {
            targetGrid = FindObjectOfType<StageManager>().stageGrid;
        }
        void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, float.MaxValue, terrainLayerMask))
            {
                active = true;
                Vector2Int hitPosition = targetGrid.GetGridPossition(hit.point);
                if (hitPosition != positionOnGrid)
                {
                    positionOnGrid = hitPosition;
                }
            }
            else
            {
                active = false;
            }
        }
    }
}