using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDS.TRPG;

namespace GDS.TRPG
{
    public class GridControl : MonoBehaviour
    {
        [SerializeField] GridMap targetGrid;
        [SerializeField] LayerMask terrainLayerMask;
        [SerializeField] GridObject hoveringOver;
        [SerializeField] SelectableGridObject selectedObject;

        Vector2Int currentGridPosition = new Vector2Int(-1, -1);

        private void Update()
        {
            HoverOverObjectCheck();
            SelectObject();
            DeselectObject();
        }
        private void HoverOverObjectCheck()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, float.MaxValue, terrainLayerMask))
            {
                Vector2Int gridPosition = targetGrid.GetGridPossition(hit.point);

                if (gridPosition == currentGridPosition) { return; }
                currentGridPosition = gridPosition;

                GridObject gridObject = targetGrid.GetPlacedObject(gridPosition);
                hoveringOver = gridObject;
            }
        }
        private void SelectObject()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (hoveringOver == null) { return; }
                selectedObject = hoveringOver.GetComponent<SelectableGridObject>();
            }
        }
        private void DeselectObject()
        {
            if (Input.GetMouseButtonDown(1))
            {
                selectedObject = null;
            }
        }
    }
}