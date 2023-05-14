using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using GDS.TRPG;

namespace GDS.TRPG
{
    public class Marker : MonoBehaviour
    {
        [SerializeField] Transform marker;
        GridMap targetGrid;
        [SerializeField] float elevation = 2f;

        MouseInput mouseInput;
        Vector2Int currentPosition;
        bool active;

        void Awake()
        {
            mouseInput = GetComponent<MouseInput>();
        }
        private void Start()
        {
            targetGrid = FindObjectOfType<StageManager>().stageGrid;
        }
        void Update()
        {
            if (active != mouseInput.active)
            {
                active = mouseInput.active;
                marker.gameObject.SetActive(active);
            }
            if (active == false) { return; }
            if (currentPosition != mouseInput.positionOnGrid)
            {
                currentPosition = mouseInput.positionOnGrid;
                UpdateMarker();
            }
        }
        private void UpdateMarker()
        {
            if (targetGrid.CheckBoundary(currentPosition) == false) { return; }
            Vector3 worldPosition = targetGrid.GetWorldPosition(currentPosition.x, currentPosition.y, true);
            worldPosition.y += elevation;
            marker.position = worldPosition;
        }
    }
}